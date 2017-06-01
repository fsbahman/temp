var showFloatingInisightBar = true;


$(document).ready(function(){
    $('.ui.checkbox').checkbox();
    $('.ui.sidebar');

    $('#fullinsight .item').click(function(){
        $('#fullinsight .active').removeClass('active');
        $(this).addClass('active');
        $("div[data-tab=" + $(this).attr("data-tab") + "]").addClass('active');
    });
});

function showTab(tabname){
    $('#fullinsight .active').removeClass('active');
    $("div[data-tab='" + tabname + "'").addClass('active');
    $("a[data-tab='" + tabname + "'").addClass('active');
    $('#fullinsight.modal').modal('show');
}
function openitem(){
    $('#iteminsight.modal').modal('setting', 'transition', 'fade').modal('show');
}


function opencustomertab(){
    $('#customerinsight.modal').modal('setting', 'transition', 'fade').modal('show');
}


function showCustomer() {
    $('#customerinsight.modal').modal('setting', 'transition', 'fade').modal('show');
}

function showInsight() {
    $('#fullinsight.modal').modal('show');
}
function showSettings() {
    $('#settingcard.modal').modal('show');
}

$('#sidebarbutton').hide();

$("body").on("mousemove", function (event) {
    if (!showFloatingInisightBar) {
        if (event.pageX < 10) {
            $(".ui.sidebar").sidebar('show');
        }
    }
})
function setSettings() {
    showFloatingInisightBar = $("#usefloatingbutton").prop('checked');
    if (showFloatingInisightBar) {
        $("#container-floating").show();
        $(".ui.sidebar").sidebar('hide');
    } else {
        $("#container-floating").hide();
        $(".ui.sidebar").sidebar('show');
    }
}
$(function () {
    $("#floating-button").draggable({
        start: function () {
            $(".nds").hide();
        },
        drag: function () {
            var nodesToMove = [".nd1", ".nd2", ".nd3", ".nd4"];
            var topOffset = 50;
            nodesToMove.forEach(function (element) {
                $(element).css(
                    {
                        top: $("#floating-button").position().top - topOffset
                    });
                topOffset += 48;
            }, this);
            $(".nds").css(
                {
                    left: $("#floating-button").position().left - 50
                });
            $("#container-floating").css(
                {
                    right: ($(window).width() - $("#floating-button").position().left) - 100,
                    bottom: ($(window).height() - $("#floating-button").position().top) - 55
                });
            console.log($("#floating-button").position);
        },
        stop: function () {
            $(".nds").show();
        }
    });
});