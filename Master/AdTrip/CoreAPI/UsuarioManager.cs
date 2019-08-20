using DataAcess.Crud;
using Entities;
using Exceptions;
using Google.Apis.Auth;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class UsuarioManager : BaseManager
    {
        private UsuarioCrudFactory crudUsuario;

        public UsuarioManager()
        {
            crudUsuario = new UsuarioCrudFactory();
        }

        public async Task CreateAsync(Usuario usuario)
        {
            try
            {
                var c = crudUsuario.Exists<Usuario>(usuario);

                if (c != null)
                {
                    // Ya existe un usuario con esos datos
                    throw new BussinessException(10);
                }

                crudUsuario.Create(usuario);

                var mng = new Rol_UsuarioManager();
                foreach (string rol in usuario.Roles)
                {
                    var rolUsuario = new Rol_Usuario
                    {
                        IdRol = rol,
                        IdUsuario = usuario.Identificacion
                    };

                    mng.Create(rolUsuario);
                }

                Response response = await EnviarCorreoManager.GetInstance().ExecuteVerificacionUsuario(usuario);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Usuario> RetrieveAll()
        {
            return crudUsuario.RetrieveAll<Usuario>();
        }

        public Usuario RetrieveById(Usuario usuario)
        {
            Usuario c = null;
            try
            {
                c = crudUsuario.Retrieve<Usuario>(usuario);
                if (c == null)
                {
                    //No existe
                    throw new BussinessException(12);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public Usuario Validar(Usuario usuario)
        {
            Usuario c = null;
            try
            {
                c = crudUsuario.Exists<Usuario>(usuario);

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public async Task<Usuario> ValidarUsuarioGoogleAsync(string idToken)
        {
            var usuario = new Usuario();
            try
            {
                var validPayload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                if (validPayload != null)
                {
                    usuario.Correo = validPayload.Email;

                    usuario = crudUsuario.ValidarUsuarioGoogle<Usuario>(usuario);
                }
                if (usuario == null)
                {
                    throw new BussinessException(11);
                }
                if (!usuario.ValorEstado.Equals("1"))
                {
                    throw new BussinessException(8);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            usuario.Roles = GetRolesUsuario(usuario.Identificacion);
            usuario.Contrasenna = null;
            return usuario;
        }

        public Usuario ValidarUsuario(Usuario usuario)
        {
            Usuario c = null;
            try
            {
                c = crudUsuario.ValidarUsuario<Usuario>(usuario);
                if (c == null)
                {
                    //El usuario no existe
                    throw new BussinessException(7);
                }
                else
                {
                    c.Contrasenna = null;
                    switch (c.ValorEstado)
                    {
                        case "3":
                            //El usuario existe pero aún no ha verificado su cuenta
                            throw new BussinessException(8);
                        case "2":
                            //El usuario existe pero su contraseña ha expirado
                            throw new BussinessException(9);
                        case "0":
                            //El usuario existe pero se encuentra inactivo
                            throw new BussinessException(9);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            c.Roles = GetRolesUsuario(c.Identificacion);
            c.Contrasenna = null;
            return c;
        }

        public void ModificarContrasenna(Usuario usuario)
        {
            try
            {
                var usuarioActual = crudUsuario.ValidarUsuario<Usuario>(usuario);
                if (usuarioActual == null)
                {
                    //El usuario no existe/Contraseña no coincide
                    throw new BussinessException(6);
                }
                else
                {
                    usuarioActual.Contrasenna = usuario.ContrasennaNueva;
                    crudUsuario.UpdateContrasenna(usuarioActual);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public async Task RestablecerContrasennaAsync(Usuario usuario)
        {
            try
            {
                Usuario us = crudUsuario.Retrieve<Usuario>(usuario);
                if (us == null)
                {
                    //El usuario no existe
                    throw new BussinessException(12);
                }
                else if(!usuario.Correo.Equals(us.Correo))
                {
                    //Datos incorrectos
                    throw new BussinessException(6);
                }
                else
                {
                    string nuevaContrasenna = RandomString(8);
                    us.Contrasenna = nuevaContrasenna;
                    crudUsuario.UpdateContrasenna(us);

                    Response response = await EnviarCorreoManager.GetInstance().ExecuteRestablecerContrasenna(us);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(Usuario usuario)
        {
            crudUsuario.Update(usuario);
        }

        public void Delete(Usuario usuario)
        {
            crudUsuario.Delete(usuario);
        }

        public bool VerificarCorreo(string id)
        {
            Usuario usuario = new Usuario()
            {
                Identificacion = id
            };

            try
            {
                usuario = crudUsuario.Retrieve<Usuario>(usuario);
                if (usuario == null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            usuario.ValorEstado = "1";
            crudUsuario.UpdateEstado(usuario);

            return true;
        }

        private string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        private string[] GetRolesUsuario(string id)
        {
            var mng = new Rol_UsuarioManager();
            var rolUsuario = new Rol_Usuario
            {
                IdUsuario = id
            };

            String[] roles = mng.RetrieveAllById(rolUsuario).Select(p => p.IdRol).ToArray();
            return roles;
        }
    }
}
