// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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