$(document).ready(function () {
    $("#brandLink").removeClass("hidden");
    $("#catalogLink").addClass("hidden");

    let selectedCARS = "ALL";
    let selectedCOLOR = 0;
    let selectedVolEngines = 0;
    let selectedItem = 5;

    $("#tileName").text(selectedCARS);

    let minPrice;
    let maxPrice;

    let selectedMinPrice;
    let selectedMaxPrice;

    let selectedDate;
    let date = new Date();
    let time = date.getFullYear() + "/" + (date.getMonth() + 1).toString() + "/" + (date.getDate() + 1).toString();
    selectedDate = time;

    $("#datepicker")
        .datepicker({
            autoclose: true,
            dateFormat: 'yy/mm/dd',
        })
        .datepicker('setDate', selectedDate)
        .on('change Date', function () {
            selectedDate = this.value;
            getListOfCars();
        });

    $("#reset").on("click", function () {
        //location.reload();
        selectedCARS = "ALL";
        var root = $('#jstree').jstree('j1_1_anchor', 'ul > li:first'); 
        $("#tileName").text(selectedCARS);
        $('#jstree').jstree("close_all");

        selectedCOLOR = 0;
        $("#colorsSelect").val(selectedCOLOR);

        selectedVolEngines = 0;
        $("#volEngineSelect").val(selectedVolEngines);

        selectedItem = 5;
        $("#select").val(selectedItem);

        selectedMinPrice = minPrice;
        selectedMaxPrice = maxPrice;

        $("#slider").slider("option", { min: minPrice, max: maxPrice });
        $("#slider > div").css({ "left": "0%", "width": "0%" });
        $("#slider > a").first().css("left", "0%");
        $("#slider > a").last().css("left", "100%");

        $("#amount").val("$" + minPrice + " - $" + maxPrice);

        selectedDate = time;
        $("#datepicker").datepicker('setDate', selectedDate);

        getListOfCars();
    });
    //---------------------------------------------------------------------
    $.when($.ajax("/Catalog/GetMinMaxPrices")).done(function (data) {
        let prices = data.split(" ");
        minPrice = Number(prices[0].split('.')[0]);
        selectedMinPrice = minPrice;
        maxPrice = Number(prices[1].split('.')[0]) + 1;
        selectedMaxPrice = maxPrice;

        $("#slider").slider("option", { min: minPrice, max: maxPrice });
        $("#slider > a").first().css("left", "0%");
        $("#slider > a").last().css("left", "100%");

        $("#amount").val("$" + minPrice + " - $" + maxPrice);

        getListOfCars();
    })

    $("#slider").slider({
        range: true,
        step: 1,
        slide: function (event, ui) {
            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
        },
        stop: function (event, ui) {
            selectedMinPrice = ui.values[0];
            selectedMaxPrice = ui.values[1];

            getListOfCars();
        }
    });

    //---------------------------------------------------------------------
    $("#jstree").jstree({
        "core": {
            "dblclick_toggle": false
        },
        "plugins": ["wholerow"]
    })
        .on('click', '.jstree-anchor', function (e) {
            $('#jstree').jstree(true).toggle_node(e.target);
        })
        .bind("select_node.jstree", function (e, data) {
            var elected = $(this).jstree().get_selected(true)[0].text;
            var VRegExp = new RegExp(/\s+/g);
            elected = elected.replace(VRegExp, '');

            if (selectedCARS != elected) {
                selectedCARS = elected;
                $("#tileName").text(selectedCARS);
                getListOfCars();
            }
        });

    var colors = $("#colorsSelect > option");
    colors.each(function (i, value) {
        $(value).css("color", value.text);
    });

    $("#colorsSelect").change(function () {
        var sel = $(this).val();
        selectedCOLOR = sel;
        $(this).css("color", $("option:selected", this).html());
        $('#colorsSelect option').each(function () {
            if (this.hasAttribute('selected'))
                this.removeAttribute("selected");
            else if ($(this).val() == sel)
                $(this).attr('selected', 'selected');
        });
        getListOfCars();
    })
    $("#volEngineSelect").change(function () {
        var sel = $(this).val();
        selectedVolEngines = sel;
        $('#volEngineSelect option').each(function () {
            if (this.hasAttribute('selected'))
                this.removeAttribute("selected");
            else if ($(this).val() == sel)
                $(this).attr('selected', 'selected');
        });
        getListOfCars();
    })

    function getListOfCars() {
        $.ajax({
            url: "/Catalog/GetCars",
            type: "POST",
            data: {
                treeNodeName: selectedCARS,
                color: selectedCOLOR,
                volEngine: selectedVolEngines,
                colsNum: selectedItem,
                minPrice: selectedMinPrice,
                maxPrice: selectedMaxPrice,
                dateTime: selectedDate
            },
            success: function (data) {
                var carsDiv = $("#findCars");
                carsDiv.empty();
                carsDiv.append(data);
            },
            error: function (data) {
                alert("Get cars fault");
            }
        });
    }

    //---------------------------------------------------------------------

    $("#select").change(function () {
        let sel = $(this).val();
        selectedItem = sel;
        $('#select option').each(function () {
            if (this.hasAttribute('selected'))
                this.removeAttribute("selected");
            else if ($(this).val() == sel)
                $(this).attr('selected', 'selected');
        });

        rebuildCarsList();
    })

    function rebuildCarsList() {
        var temp = $(".div-tile");
        var table = $("<div class='div-tile-table'></div>");

        for (var i = 0; i < temp.length;) {
            var row = $("<div class='div-tile-row'></div>");
            for (var j = 0; j < selectedItem; j++) {
                var cell = $("<div class='div-tile-cell'></div>");
                cell.append(temp[i++]);
                row.append(cell);
            }
            table.append(row);
        }

        $("#findCars").empty();
        $("#findCars").append(table);
    }
});