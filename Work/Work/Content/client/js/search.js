var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $("#txtsearch").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (data) {
                        response(data.data); // Fix: use data.data instead of response.data
                    }
                });
            },
            focus: function (event, ui) {
                console.log(ui)
                $("#txtsearch").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtsearch").val(ui.item.label);
               
                return false;
            },


        })
            .autocomplete("instance")._renderItem = function (ul, item) {
              
                return $("<li>")
                    .append("<div>" + item.label + "</div>") // Fix: close the anchor tag properly
                    .appendTo(ul);
            };
    }
};

common.init();