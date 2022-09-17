
//فانکشی که برای اینکه تصویر انتخاب شد همون لحظه تغییر بده fileUpload
function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgAvatar').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#createUserViewModel_UserAvatar").change(function () {
    readURL(this);
});


//***


