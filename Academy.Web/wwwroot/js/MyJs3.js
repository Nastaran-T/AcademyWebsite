$("#Course_GroupId").change(function () {
    $("#Course_ParentId").empty();
    $.getJSON("/home/GetSubGroups/" + $("#Course_GroupId :selected").val(),
        function (data) {

            $.each(data,
                function () {
                    $("#Course_ParentId").append('<option value=' + this.value + '>' + this.text + '</option>');

                });

        });


});