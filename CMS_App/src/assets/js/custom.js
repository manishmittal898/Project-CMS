
$(function() {
  setTimeout(() => {
    $(".sidebar-menu-btn").click(function () {
      $("body").toggleClass("sidebar-open");
    });
  },
  10);
 });

 $(function() {
  setTimeout(() => {
    $("ul.sidebar-submenu a, .sidebar-item-button.arrow-none").click(function () {
      $("body").removeClass("sidebar-open");
    });
  },
  10);
 });




