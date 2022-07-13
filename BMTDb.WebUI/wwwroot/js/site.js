// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/******************** Close Popup ********************/

$(".msgbox-close").click(function () {
    $(".msgbox").fadeOut("fast");
});

/******************** Close Popup ********************/


/************** Hide Navbar When Scroll **************/

var firstScrollPosition = window.pageYOffset;                       //Assigns page height to 'firstScrollPosition'

window.onscroll = function ()                           
{
    var currentScrollPosition = window.pageYOffset;
    if (firstScrollPosition > currentScrollPosition) {
        document.getElementById("navbar").style.top = "0";          //Shows navbar
    } else {
        document.getElementById("navbar").style.top = "-60px";      //Hides Navbar
    }
    firstScrollPosition = currentScrollPosition;                    //Update scroll position
}

/************** Hide Navbar When Scroll **************/


/************** Dark Mode Script **************/

let darkMode = localStorage.getItem('darkMode');        // check for saved 'darkMode' in localStorage

const darkModeToggle = document.querySelector('#dark-mode-toggle');

const enableDarkMode = () => {

    document.body.classList.add('darkmode');            //Add the class to the body
    localStorage.setItem('darkMode', 'enabled');        //Update darkMode in localStorage
}

const disableDarkMode = () => {

    document.body.classList.remove('darkmode');         // Remove the class from the body
    localStorage.setItem('darkMode', null);             // Update darkMode in localStorage 
}

if (darkMode === 'enabled') {           // If the user already visited and enabled darkMode

    enableDarkMode();                   // start things off with it on
}

darkModeToggle.addEventListener('click', () => {        // When someone clicks the button

    darkMode = localStorage.getItem('darkMode');        // get their darkMode setting

    if (darkMode !== 'enabled') {       // if it not current enabled, enable it
        enableDarkMode();
    }
    else {                              // if it has been enabled, turn it off
        disableDarkMode();
    }
}
);

/************** Dark Mode Script **************/


/************** Accordion Script **************/

document.querySelectorAll('.accordion_button').forEach(button => {
    button.addEventListener('click', () => {
        const accordionContent = button.nextElementSibling;

        button.classList.toggle('accordion_button--active');

        if (button.classList.contains('accordion_button--active')) {
            accordionContent.style.maxHeight = accordionContent.scrollHeight + 'px';
        }
        else {
            accordionContent.style.maxHeight = 0;
        }
    });
});

/************** Accordion Script **************/


/************* Random Background *************/

function randomBackground() {
    var images = [
        '../img/background/background01.jpg',
        '../img/background/background02.jpg',
        '../img/background/background03.jpg',
        '../img/background/background04.jpg',
        '../img/background/background05.jpg',
        '../img/background/background06.jpg',
        '../img/background/background07.jpg',
        '../img/background/background08.jpg',
        '../img/background/background09.jpg',
        '../img/background/background10.jpg',
        '../img/background/background11.jpg',
        '../img/background/background12.jpg',
        '../img/background/background13.jpg',
        '../img/background/background14.jpg',
        '../img/background/background15.jpg',
        '../img/background/background16.jpg',
        '../img/background/background17.jpg',
        '../img/background/background18.jpg',
        '../img/background/background19.jpg',
        '../img/background/background20.jpg',
        '../img/background/background21.jpg',
        '../img/background/background22.jpg',
        '../img/background/background23.jpg',
        '../img/background/background24.jpg',
        '../img/background/background25.jpg',
        ];
    var random = Math.floor(images.length * Math.random());
    //var element = document.getElementsByClassName('background');                  //Background-Image
    //element[0].style["background-image"] = "url(" + images[random] + ")";
    document.getElementById('background').innerHTML = "<img class=\"user-background\" src=" + images[random] + ">";
}

document.addEventListener("DOMContentLoaded", randomBackground);

/************* Random Background *************/

/************* Scroll Buttons *************/

const buttonRight = document.querySelectorAll(".right-scroll");
const buttonLeft = document.querySelectorAll(".left-scroll");
const scroll = document.querySelectorAll(".home-display");

buttonRight.forEach((item, index) => {
    item.addEventListener("click", () => {
        scroll[index].scrollLeft += 110;
    });
});

buttonLeft.forEach((item, index) => {
    item.addEventListener("click", () => {
        scroll[index].scrollLeft -= 110;
    });
});

/************* Scroll Buttons *************/
