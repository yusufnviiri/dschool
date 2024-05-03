// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var teste = document.getElementById("devo");
teste.innerHTML = "i love Annitah"

function noe() {
    var teste = document.getElementById("devo");
    teste.classList.add("noel")
}
function showBars() {
    var bars = document.getElementById("bars");
    var cross = document.getElementById("xmark");
    if (bars!=null) {
        bars.classList.add("hide_bars")
        cross.classList.add("show_cross")
        cross.style.display = "block";
    }
    if (cross!=null) {
        cross.classList.add("show_cross")
    }

    console.log(bars)
    console.log(cross)

}

function showCross() {
    var bars = document.getElementById("bars");
    var cross = document.getElementById("xmark");
    if (bars != null) {
        bars.classList.add("show_cross")
    }
    if (cross != null) {
        cross.classList.add("hide_cross")
    }
    console.log(bars)
    console.log(cross)


}