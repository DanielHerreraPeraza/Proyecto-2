//function Pago(dt) {
//    paypal.Button.render({
//        // Configure environment
//        env: 'sandbox',
//        client: {
//            sandbox: 'AXgCkOz_JWQZPDVMFwqQZuroSlMI1LMHLdS78A2CmD1VgqB2ikwjAanRpzKwpUzoFHS_jspaN-LKBTL7',
//            production: 'demo_production_client_id'
//        },
//        // Customize button (optional)
//        locale: 'es_CR',
//        style: {
//            size: 'medium',
//            color: 'blue',
//            shape: 'pill',
//            height: 40
//        },

//        // Enable Pay Now checkout flow (optional)
//        commit: true,

//        // Set up a payment
//        payment: function (data, actions) {
//            return actions.payment.create({
//                transactions: [{
//                    amount: {
//                        total: dt.total,
//                        currency: 'USD',
//                        details: {
//                            subtotal: '70.00',
//                            tax: '20',
//                            shipping: '10',
//                            handling_fee: '0',
//                            shipping_discount: '0',
//                            insurance: '0'
//                        }
//                    }
//                }]
//            });
//        },
//        // Execute the payment
//        onAuthorize: function (data, actions) {
//            return actions.payment.execute().then(function () {
//                // Show a confirmation message to the buyer
//                window.alert('Thank you for your purchase!');
//            });
//        }
//    }, '#paypal-button');

//};

//$(document).ready(function () {
//    var data = {};
//    data['total'] = 100;
//    data['subtotal'] = 40;
//    data['tax'] = 10;

//    var pago = new Pago(data);
//});