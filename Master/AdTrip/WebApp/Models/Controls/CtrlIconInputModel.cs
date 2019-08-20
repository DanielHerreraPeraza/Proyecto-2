namespace WebApp.Models.Controls
{
    public class CtrlIconInputModel : CtrlBaseModel
    {
        public string Type { get; set; }
        public string PlaceHolder { get; set; }
        public string ColumnDataName { get; set; }
        public string Icon { get; set; }
        public bool Required { get; set; }

        public CtrlIconInputModel()
        {
            ViewName = "";
        }
    }
}