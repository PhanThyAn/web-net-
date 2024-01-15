$(document).ready(function () {
    const reg_mail = /^[A-Za-z0-9]+[_\.\-]?[A-Za-z0-9]*@[A-Za-z0-9]+[-\.\_]{1}[A-Za-z0-9]+[\.]?[A-Za-z]*[\.]?[A-Za-z]*$/;
    // const reg_pass = /^[a-zA-Z0-9!@#$%^&*()_.?\/]{6,}$/;
    const email = $('#email');
    // const new_pass = $('#new-pass');
    // const c_new_pass = $('#c-new-pass');
    let flag = true;
    $('#form-forget').submit(function () {
        flag = true;
        if (email.val() !== '') {
            if (!(reg_mail.test(email.val())) && email.val() !== undefined) {
                notEmpty(email, 'Sai định dạng Email', email.val());
                flag = false;
            }
        } else {
            if (email.val() === '') {
                notEmpty(email, 'Vui lòng nhập Email', null);
                flag = false;
            }
        }
       

        return flag;
    });

    function notEmpty(ele, text, value) {
        ele.attr('placeholder', text).val('').addClass('holder-danger').on('focus', function () {
            if (value !== null) {
                ele.val(value);
            } else {
                ele.attr('placeholder', '');
            }
        }).on('blur', function () {
            ele.val(ele.val());
            ele.attr('placeholder', text);
        });
    }
});