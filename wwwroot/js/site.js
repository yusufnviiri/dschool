// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('click', e => {
    const isDropDownButton = e.target.matches('[data-dropdown-button]')
    if (!isDropDownButton && e.target.closest('[data-dropdown]') != null) return
    let currentDropdown

    if (isDropDownButton) {
        currentDropdown = e.target.closest('[data-dropdown]') 
        currentDropdown.classList.toggle('active')         
    }
    document.querySelectorAll('[data-dropdown].active').forEach(dropdown => {
        if (dropdown === currentDropdown) return
        dropdown.classList.remove('active')
    })
})

const bar = document.getElementById('bars');
const menu = document.getElementById('header')
const dropDownLists = document.getElementsByClassName('dropdown-menu')

function toggleHeader() {
    menu.classList.toggle('active')
    
}

function payUniform() {
    const uniformField = document.getElementById('uniform-amount')
    uniformField.classList.toggle('setUniform');
}