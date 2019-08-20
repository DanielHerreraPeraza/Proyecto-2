(function ($) {

    "use strict";

    // Cache Selectors
    var mainWindow = $(window),
        mainDocument = $(document),
        myLoader = $(".loader"),
        mytopBar = $('#top-bar'),
        menuBtn = $("#menu-button"),
        // overlay			=$(".overlay"),
        colorPanel = $('#colorPanel');


    // Loader
    mainWindow.on('load', function () {
        myLoader.fadeOut("slow");
    });


    // Scroll
    $(document).ready(function () {

        // Add smooth scrolling to all links
        $(".landing-page-navbar a").on('click', function (event) {

            // Make sure this.hash has a value before overriding default behavior
            if (this.hash !== "") {
                // Prevent default anchor click behavior
                event.preventDefault();

                // Store hash
                var hash = this.hash;

                // Using jQuery's animate() method to add smooth page scroll
                // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
                $('html, body').animate({
                    scrollTop: $(hash).offset().top
                }, 800, function () {

                    // Add hash (#) to URL when done scrolling (default click behavior)
                    window.location.hash = hash;
                });
            } // End if
        });
    });

})(jQuery);

// *************Navigation Animation************

$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('nav').addClass('height');
    } else {
        $('nav').removeClass('height');
    }
})
$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('nav .container').addClass('padding');
    } else {
        $('nav .container').removeClass('padding');
    }
})

$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('nav#mynavbar').addClass('black-color');
    } else {
        $('nav#mynavbar').removeClass('black-color');
    }
})

$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('nav#mynavbar.bg-light').addClass('black-color');
    } else {
        $('nav#mynavbar.bg-light').removeClass('black-color');
    }
})

$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('nav.navbar#mynavbar.navbar-custom.navbar-transparent').addClass('white-color');
    } else {
        $('nav.navbar#mynavbar.navbar-custom.navbar-transparent').removeClass('white-color');
    }
})

$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('.main-navbar#mynavbar .navbar-nav a').addClass('black-color1');
    } else {
        $('.main-navbar#mynavbar .navbar-nav a').removeClass('black-color1');
    }
})

$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('.main-navbar#mynavbar a.navbar-brand').addClass('black-color1');
    } else {
        $('.main-navbar#mynavbar a.navbar-brand').removeClass('black-color1');
    }
})

$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('.main-navbar#mynavbar nav.navbar#mynavbar .navbar-search-link > li:last-child a').addClass('black-color1');
    } else {
        $('.main-navbar#mynavbar nav.navbar#mynavbar .navbar-search-link > li:last-child a').removeClass('black-color1');
    }
})



$(window).on('scroll', function () {
    if ($(window).scrollTop()) {
        $('nav.navbar.main-navbar#mynavbar-2.bg-light').addClass('full-width');
    } else {
        $('nav.navbar.main-navbar#mynavbar-2.bg-light').removeClass('full-width');
    }
})


// ***********Custom SideBar & SideNav************

$(document).ready(function () {
    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    $('#dismiss, .overlay').on('click', function () {
        $('#sidebar').removeClass('active');
        $('.overlay').removeClass('active');
    });

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').addClass('active');
        $('.overlay').addClass('active');
        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });
});

// ***********Nav Tabs Switching to work proplorly************

$(".nav-pills .nav-item .nav-link:not(.nav-pills .nav-item .nav-link), .nav-tabs").click(function () {
    $("ul.nav.nav-tabs li.nav-item.active").removeClass('active');
});


// *********************Landing Page links Switching fix***********************

$(".nav-links-active .navbar-nav li.nav-item").click(function () {
    $(".nav-links-active .navbar-nav li.nav-item.active").removeClass('active');
    $(this).addClass('active');
});