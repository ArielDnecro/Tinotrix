
jQuery(document).ready(function () {

    /*
        Fullscreen background
    */
    $.backstretch([
         "../Images/BackGround/fondoazul.png"
        , "../Images/BackGround/fondogris.png"
        , "../Images/BackGround/fondomorado.png"
        , "../Images/BackGround/fondonaranja.png"
        , "../Images/BackGround/fondorojo.png"
        , "../Images/BackGround/fondoturquesa.png"
        , "../Images/BackGround/fondoverde.png"
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