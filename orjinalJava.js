let slideIndex = 0;// related to the slid show at the home page at the top
showSlides();

function showSlides() {
  let i;
  const slides = document.getElementsByClassName("mySlides");

  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }

  slideIndex++;
  if (slideIndex > slides.length) { slideIndex = 1 }

  slides[slideIndex - 1].style.display = "block";

  setTimeout(showSlides,3000);
}
/////////////////////////////////////////////////////////
//here is the array that i will store the email and the password in
var userData = [];

function signUp(event) 
{
    event.preventDefault();  // Prevents the default form submission
    console.log("Before redirection");
      // Get input values
      var email = document.getElementById('email').value;
      var password = document.getElementById('password').value;
      var confirmPassword = document.getElementById('Comfirm-password').value;

        // Check if passwords match
        if (password !== confirmPassword) {
            alert("Passwords do not match!");
            return;
        }

        // Check if email already exists
        if (isEmailExist(email)) {
            alert("Email already exists! Please use a different email.");
            return;
        }

        // Push the values to the userData array
        userData.push([email, password]);

        // Redirect to the login page
        console.log("After redirection");    
        window.location.replace('login.html');
}

  function isEmailExist(email)
{
      // Check if email already exists in the array
      for (var i = 0; i < userData.length; i++) {
          if (userData[i][0] === email) {
              return true; // Email already exists
          }
      }
      return false; // Email does not exist
}
function scrollToTop() {
  document.body.scrollTop = 0;/*This line sets the scroll position of the body element to 0*/
  document.documentElement.scrollTop=0;/*sets the scroll position of the html to 0*/
}