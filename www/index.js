$(document).ready(start);

function start()
{
    $("#linkform").submit(submitted);
    $("#loading").hide();
    $("#link").hide();
}

function submitted(e)
{
    e.preventDefault();
    
    let link = $("#youtubelink").val();
    let name = $("#filename").val();

    $("#dataform").fadeOut(300, function () { $("#loading").fadeIn(300, function () { SoulApp.DownloadSubs(link, name); }); });
}

function downloadFailed()
{
    $("#dataform").show();
    $("#loading").hide();
    window.alert("Error Occured");
}

function downloadOK(a) {
    $("#dataform").show();
    $("#loading").hide();
    window.alert(a);
}