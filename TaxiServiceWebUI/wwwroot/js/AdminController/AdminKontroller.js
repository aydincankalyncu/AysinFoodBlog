function TestMethod() {
    var jsonData = { queryString: $("#tableSearch").val() };
    $.ajax({
        type: "POST",
        url: "Food/GetFoodInfo",
        dataType: 'json',
        data: jsonData,
        success: function (data) {
            $("#tableBody").empty();
            if (data) {
                for (var index = 0; index < data.length; index++) {
                    $("#tableBody").append(
                        "<tr>" +
                        "<td>" + data[index].id + "<td>" +
                        "<td>" + data[index].foodTitle + "<td>" +
                        "<td>" + data[index].foodImage + "<td>" +
                        "<td>" + data[index].categoryName + "<td>" +
                        "<td>" + data[index].foodPublishDate + "<td>" +
                        "<td><a class='btn btn-info' asp-controller='Food' asp-action='Update' asp-route-foodId='" + data[index].id + "'>Güncelle</a><td>" +
                        "<td><a class='btn btn-info' asp-controller='Food' asp-action='Delete' asp-route-foodId='" + data[index].id + "'>Sil</a><td>" +
                        "</tr > "
                    );
                }
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