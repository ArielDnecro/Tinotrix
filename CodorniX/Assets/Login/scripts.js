
jQuery(document).ready(function () {

    /*
        Fullscreen background
    */
    $.backstretch([
                    "../Images/BackGround/2.jpg"
                     , "../Images/BackGround/3.png"
                     , "../Images/BackGround/1.jpg"
    ], { duration: 3000, fade: 750 });

    /*
        Form validation
    */
    $('.login-form asp:TextBox[type="text"], .login-form asp:TextBox[type="password"], .login-form textarea').on('focus', function () {
        $(this).removeClass('input-error');
    });

    $('.login-form').on('submit', function (e) {

        $(this).find('asp:TextBox[type="text"], asp:TextBox[type="password"], textarea').each(function () {
            if ($(this).val() == "") {
                e.preventDefault();
                $(this).addClass('input-error');
            }
            else {
                $(this).removeClass('input-error');
            }
        });

    });


});