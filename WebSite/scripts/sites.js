var site = (function ($) {
    return {
        init: function () {
            $('#Get').click(function () {
                site.getData();
            });

            $('#Submit').click(function () {
                site.submit();
            });
        },

        getData: function () {
            var api = $("#api-url").val();
            if ($('#matchId-pro').val() === "") {
                alert("Please input matchId.");
                return;
            }

            var param = {
                MatchId: $('#matchId-pro').val()
            }

            $.ajax({
                type: "GET",
                async: false,
                crossDomain: true,
                headers: { 'Access-Control-Allow-Origin': '*' },
                url: api + "api/clone-data/get-result",
                dataType: "json",
                success: function (result) {
                    if (result.ResultDetails.length > 0 || result.ExternalResultDetails.length > 0) {
                        $('#data-result-detail').empty();
                        $('#data-external-result-detail').empty();
                        $('#data-result-detail').show();
                        $('#data-external-result-detail').show();
                        $('#data-result-detail').append("Result Detail:");
                        $('#data-result-detail').append(JSON.stringify(result.ResultDetails));
                        $('#data-external-result-detail').append("External Result:");
                        $('#data-external-result-detail').append(JSON.stringify(result.ExternalResultDetails));
                    } else {
                        alert("No data on PRO to clone.");
                    }
                },
            });
        },

        submit: function () {
            var api = $("#api-url").val();
            if ($('#matchId-pro').val() === "" || $('#matchId-sit').val() === "") {
                alert("Please input matchId.");
                return;
            }
            $('#data-result-detail').hide();
            $('#data-external-result-detail').hide();
            var param = {
                MatchId: $('#matchId-pro').val()
            }

            $.ajax({
                type: "POST",
                crossDomain: true,
                url: api + "api/clone-data/get-result",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(param),
                success: function (result) {
                    if (result.ResultDetails.length > 0 || result.ExternalResultDetails.length > 0) {
                        site.cloneData(result);
                    } else {
                        alert("No data on PRO to clone.");
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

        cloneData: function (data) {
            data.MatchId = $('#matchId-sit').val()
            $.ajax({
                type: "POST",
                url: "api/clone-data/clone-result",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                success: function (result) {
                    if (result.ErrCode === 0) {
                        alert("The clone has completed successfully.")
                    }
                    else {
                        alert(result.ErrMsg);
                    }
                }
            });
        }
    };
}(jQuery));