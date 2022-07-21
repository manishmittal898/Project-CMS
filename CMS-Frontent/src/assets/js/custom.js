// Tooltip Start

//  $(function() {
//      $('[data-toggle="tooltip"]').tooltip()
//  })
//  $(document).ready(function() {
//      $('[data-toggle="popover"]').popover({
//          trigger: 'hover'
//      });
//   });


// Initialize tooltips
// var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
// var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
//     return new bootstrap.Tooltip(tooltipTriggerEl)
// })

// Tooltip End



// Back to top start
$(document).ready(function() {
    $(window).scroll(function() {
        if ($(this).scrollTop() > 50) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function() {
        $('body,html').animate({
            scrollTop: 0
        }, 400);
        return false;
    });

});
// Back to top End

// animation
AOS.init();


// $('#HomeSlider').carousel({
//     interval: 3000,
//     cycle: true
// });




// $(function() {
//     $("#ChangeToggle").click(function() {
//         $("#navbar-hamburger").toggleClass("hidden");
//         $("#navbar-close").toggleClass("hidden");
//     });
// });

$(window).scroll(function() {
    var scroll = $(window).scrollTop();

    if (scroll >= 300) {
        $("header").addClass("header-fix");
    } else {
        $("header").removeClass("header-fix");
    }
});



document.addEventListener("DOMContentLoaded", function(){
  document.querySelectorAll('.navbar .dropdown').forEach(function(everydropdown){
    everydropdown.addEventListener('shown.bs.dropdown', function () {
        el_overlay = document.createElement('span');
        el_overlay.className = 'screen-darken';
        document.body.appendChild(el_overlay)
    });

    everydropdown.addEventListener('hide.bs.dropdown', function () {
      document.body.removeChild(document.querySelector('.screen-darken'));
    });
  });

});
