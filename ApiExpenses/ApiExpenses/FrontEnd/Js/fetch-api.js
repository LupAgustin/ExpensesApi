const baseurl = "https://localhost:7152"

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
            localStorage.setItem('token', data);
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

            document.getElementById("signupUsername").value = "";
            document.getElementById("signupPassword").value = "";

            document.getElementById("radioLogin").checked = true;
            changeForm();

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
            
            Swal.fire("Success", "Login successful!", "success");

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
    const token = localStorage.getItem('token');
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
        });

        if (!response.ok) {
            throw new Error('Failed to fetch categories');
        }

        const categories = await response.json();
        console.log(categories);

        loadCategoryDropdown(categories);
    } catch (error) {
        console.error('Error loading categories:', error);
    }
}

function loadCategoryDropdown(categories) {
    const categorySelect = document.getElementById("categorySelect");

    categorySelect.innerHTML = "";

    const defaultOption = document.createElement("option");
    defaultOption.text = "Select a category";
    defaultOption.value = "";
    categorySelect.add(defaultOption);

    categories.forEach(category => {
        const option = document.createElement("option");
        option.text = category.name;
        option.value = category.id;
        categorySelect.add(option);
    });
}

function setTodayDate() {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');

    document.getElementById('expenseDate').value = `${year}-${month}-${day}`;
}

async function loadExpenses() {
    const token = localStorage.getItem('token');
    const url = baseurl + "/AllUserExpenses";

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


function populateExpensesTable(expenses) {
    const tableBody = document.getElementById('expensesTableBody');
    tableBody.innerHTML = '';

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

async function addExpense(quantity, category, datetime) {
    const token = localStorage.getItem('token');
    const url = 'https://localhost:7152/CreateExpense';
    
    const expense = JSON.stringify({
        categoryId: category,
        amount: quantity,
        date: datetime
    });

    try {
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: expense
        })

        //const data = await response;
        const result = await response.text();

        console.log('Response status:', result.status);

        if (!response.ok) {
            const errorText = await response.text();
            console.error('Failed to add expense:', errorText);
            return false;
        }

        console.log('Expense added successfully');
        return true;

    } catch (error) {
        console.error('Error in addExpense:', error);
        return false;
    }
}










