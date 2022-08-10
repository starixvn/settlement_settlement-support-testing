var broadCast = (function ($) {
    return {
        init: function () {
            $('#submit').click(function () {
                broadCast.submit();
            });
        },

        submit: function () {
            var api = $("#api-url").val();
            if ($('#matchId').val() === "") {
                alert("Please input matchId.");
                return;
            }
            var param = {
                MatchId: $('#matchId').val(),
                Action: $('#action').val(),
                resources: `<Resources>${broadCast.getResources()}</Resources>`
            }

            $.ajax({
                type: "POST",
                url: api + "happy-five",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(param),
                success: function (result) {
                    if (result.errorCode > 0) {
                        alert(result.errorMessage);
                    }
                },
                error: function () {
                    console.log("Error");
                },
                complete: function () {
                    console.log("Complete");
                }
            });
        },

        getResources: function () {
            var result = '';
            for (var i = 1; i <= 5; i++) {
                var value = $("#ball-" + i).val();
                if (value != '') {
                    result += `<Resource Id="${i}" Result="${value}" />`;
                }
            }
            return result;
        },
    };
}(jQuery));