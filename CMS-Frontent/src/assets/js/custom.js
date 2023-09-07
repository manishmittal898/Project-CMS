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


$(document).ready(function () {
  $(document).mousemove(function (e) {
    var x = e.clientX; var y = e.clientY;
    var x = e.clientX; var y = e.clientY;
    var imgx1 = $('.product-d-main-slider div.slick-active .slideshow-items').offset().left;
    var imgx2 = $('.product-d-main-slider div.slick-active .slideshow-items').outerWidth() + imgx1;
    var imgy1 = $('.product-d-main-slider div.slick-active .slideshow-items').offset().top;
    var imgy2 = $('.product-d-main-slider div.slick-active .slideshow-items').outerHeight() + imgy1;
    debugger
    if (x > imgx1 && x < imgx2 && y > imgy1 && y < imgy2 && (e.target.classList.contains('slideshow-items'))) {
      $('#lens').show(); $('#result').show();
      imageZoom($('.product-d-main-slider div.slick-active .slideshow-items'), $('#result'), $('#lens'));
    } else {
        console.log(e.target.classList)
        $('#lens').hide(); $('#result').hide();
    }
  });
});

function imageZoom(img, result, lens) {
    debugger
  result.width(img.innerWidth()); result.height(img.innerHeight());
  lens.width(img.innerWidth() / 2); lens.height(img.innerHeight() / 2);
  result.offset({ top: img.offset().top, left: img.offset().left + img.outerWidth() + 10 });
  var cx = img.innerWidth() / lens.innerWidth(); var cy = img.innerHeight() / lens.innerHeight();
  result.css('backgroundImage', 'url(' + img.attr('src') + ')');
  result.css('backgroundSize', img.width() * cx + 'px ' + img.height() * cy + 'px');
//   lens.mousemove(function (e) { moveLens(e); });
  img.mousemove(function (e) { moveLens(e); });
//   lens.on('touchmove', function () { moveLens(); })
  img.on('touchmove', function () { moveLens(); })
  function moveLens(e) {
    var x = e.clientX - lens.outerWidth() / 2;
    var y = e.clientY - lens.outerHeight() / 2;
    if (x > img.outerWidth() + img.offset().left - lens.outerWidth()) { x = img.outerWidth() + img.offset().left - lens.outerWidth(); }
    if (x < img.offset().left) { x = img.offset().left; }
    if (y > img.outerHeight() + img.offset().top - lens.outerHeight()) { y = img.outerHeight() + img.offset().top - lens.outerHeight(); }
    if (y < img.offset().top) { y = img.offset().top; }
    lens.offset({ top: y, left: x });
    result.css('backgroundPosition', '-' + (x - img.offset().left) * cx + 'px -' + (y - img.offset().top) * cy + 'px');
  }
}
