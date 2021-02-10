// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


function myFunction(elem) {
    var x = elem.parentNode;
    x.style.visibility = "hidden";
}

function removeFault(carId, fault, elem) {
    var test = elem;
    $.ajax({
        method: "POST",
        url: "/Car/RemoveFault",
        type: "text",
        data: {
            fault: fault,
            carId: carId,
        },
        success: function (test) {
            var x = elem.parentNode;
            x.style.visibility = "hidden";
        }
    });
}
