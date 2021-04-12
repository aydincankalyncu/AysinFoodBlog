function UpdateComments() {
    var jsonData = { foodId: $("#foodId").val(), name: $("#Name").val(), comment: $("#Message").val() };
    $.ajax({
        type: "POST",
        url: "/Food/MakeComment",
        dataType: 'json',
        data: JSON.stringify(jsonData),
        success: function (data) {
            $("#tableBody").empty();
            if (data) {
                
            }
        },

        error: function () {
            alert("Hata Oluştu");
        },
        onbeforeclose: function () {
            return onbeforeclose();
        }
    });
}