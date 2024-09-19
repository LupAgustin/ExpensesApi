const baseurl = "https://localhost:7152"
// Function to handle Signup
async function signUp(name, pass) {
    const url = baseurl + "/SignUp"
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: name,
                password: pass
            })
        });
        const data = await response;
        if (response.ok) {
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        return false;
    }
}

async function Login(name, pass){
    try {
        const url = baseurl + "/Login"
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: name,
                password: pass
            })
        });
        const data = await response.text();
        console.log(data);
        if (response.ok) {
            localStorage.setItem('token', data); // Store JWT Token
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error:', error);
        return false;
    }
}

function changeForm() {
    $("#signupradio").toggleClass("d-none");
    $("#loginradio").toggleClass("d-none");
}

async function ClickSignUp() {
    const username = document.getElementById("signupUsername").value;
    const password = document.getElementById("signupPassword").value;

    try {
        const result = await signUp(username, password);
        console.log(result);

        if (result) {

                    // Clear the signup form fields
            document.getElementById("signupUsername").value = "";
            document.getElementById("signupPassword").value = "";

            // Switch to the login form
            document.getElementById("radioLogin").checked = true;
            changeForm();

            // Pre-fill the username in the login form
            document.getElementById("loginUsername").value = username;


            Swal.fire("Success", "Signup successful!", "success");
        } else {
            Swal.fire("Error", "Signup failed! Please try again.", "error");
        }
    } catch (error) {
        console.error('Error:', error);
        Swal.fire("Error", "Something went wrong. Please try again.", "error");
    }
}

async function ClickLogin() {
    const username = document.getElementById("loginUsername").value;
    const password = document.getElementById("loginPassword").value;

    try {
        const token = await Login(username, password);
        console.log(token);

        if (token) {
            
            // Show success message
            Swal.fire("Success", "Login successful!", "success");

            // Redirect to the dashboard page
            setTimeout(()=> {
                window.location.href = "Dashboard.html";
            }, 1000)            
        } else {
            Swal.fire("Error", "Login failed! Please check your credentials.", "error");
        }
    } catch (error) {
        console.error('Error:', error);
        Swal.fire("Error", "Something went wrong. Please try again.", "error");
    }
}


async function GetCategories() {
    const token = localStorage.getItem('token'); // Retrieve JWT Token
    if (!token) {
        console.error("User is not logged in.");
        return;
    }
    const url = baseurl + "/GetCategories"
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        }); // Your API endpoint for categories

        if (!response.ok) {
            throw new Error('Failed to fetch categories');
        }

        const categories = await response.json(); // Assuming the response is JSON
        console.log(categories); // Log categories to ensure it's correct

        loadCategoryDropdown(categories); // Function to populate HTML with categories
    } catch (error) {
        console.error('Error loading categories:', error);
    }
}

function loadCategoryDropdown(categories) {
    const categorySelect = document.getElementById("categorySelect"); // Assume you have a <select> element with id "categorySelect"

    // Clear existing options
    categorySelect.innerHTML = "";

    // Add a default option
    const defaultOption = document.createElement("option");
    defaultOption.text = "Select a category";
    defaultOption.value = "";
    categorySelect.add(defaultOption);

    // Add categories as options
    categories.forEach(category => {
        const option = document.createElement("option");
        option.text = category.name; // Assuming the category object has a 'name' property
        option.value = category.id;  // Assuming the category object has an 'id' property
        categorySelect.add(option);
    });
}

// Function to format today's date as yyyy-mm-dd
function setTodayDate() {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const day = String(today.getDate()).padStart(2, '0');

    // Set the value of the date input to today's date in yyyy-mm-dd format
    document.getElementById('expenseDate').value = `${year}-${month}-${day}`;
}


// Function to load expenses from the API and populate the table
async function loadExpenses() {
    const token = localStorage.getItem('token'); // Retrieve JWT token
    const url = baseurl + "/AllUserExpenses"; // API endpoint

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch expenses');
        }

        const expenses = await response.json();
        populateExpensesTable(expenses);
        calculateTotalAmount(expenses);

    } catch (error) {
        console.error('Error loading expenses:', error);
    }
}

// Function to populate the expenses table
function populateExpensesTable(expenses) {
    const tableBody = document.getElementById('expensesTableBody');
    tableBody.innerHTML = ''; // Clear the table body

    expenses.forEach((expense, index) => {
        const row = `
            <tr>
                <td>${index + 1}</td>
                <td>$${expense.amount.toFixed(2)}</td>
                <td>${expense.category.name}</td>
                <td>${new Date(expense.date).toLocaleDateString()}</td>
                <td>
                    <button class="btn btn-sm btn-warning">Edit</button>
                    <button class="btn btn-sm btn-danger">Delete</button>
                </td>
            </tr>
        `;
        tableBody.insertAdjacentHTML('beforeend', row);
    });
}
