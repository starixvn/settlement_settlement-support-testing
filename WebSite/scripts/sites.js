var site = (function ($) {
    return {
        init: function () {
            $('#Get').click(function () {
                site.getData();
            });

            $('#Submit').click(function () {
                site.submit();
            });

            $('#get-balance').click(function () {
                site.getBalance();
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
                type: "POST",
                url: api + "api/clone-data/get-result",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(param),
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

        getBalance: function () {
            var api = $("#api-url").val();
            if ($('#matchId').val() === "" || $('#custId').val() === "") {
                alert("Please input value.");
                return;
            }

            var param = {
                MatchId: $('#matchId').val(),
                CustId: $('#custId').val()
            }

            $.ajax({
                type: "POST",
                url: "api/balance/get-balance",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(param),
                success: function (result) {
                    if (result.Table.length > 0 || result.Table1.length > 0 ||
                        result.Table2.length > 0 || result.Table3.length > 0) {
                        $("#data").empty();
                        $("#data").show();
                        $("#data").append('General Wallet: ');
                        $("#data").append(JSON.stringify(result.Table));
                        $("#data").append('<br>');
                        $("#data").append('Bonus Wallet: ');
                        $("#data").append(JSON.stringify(result.Table1));
                        $("#data").append('<br>');
                        $("#data").append('Bonus Adjustment Transfer: ');
                        $("#data").append(JSON.stringify(result.Table2));
                        $("#data").append('<br>');
                        $("#data").append('Transaction: ');
                        $("#data").append(JSON.stringify(result.Table3));
                    }
                    else {
                        $("#data").empty();
                        alert("No data ");
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