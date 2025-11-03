// Site-wide JavaScript functionality

// Auto-dismiss alerts after 5 seconds
document.addEventListener('DOMContentLoaded', function () {
    // Auto-dismiss alerts
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(alert => {
        setTimeout(() => {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });

    // Form validation enhancement
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function (e) {
            const submitButton = this.querySelector('button[type="submit"]');
            if (submitButton) {
                submitButton.disabled = true;
                submitButton.innerHTML = '<span class="loading"></span> Processing...';
            }
        });
    });

    // Animate statistics counters
    animateStatistics();
});

// Statistics counter animation
function animateStatistics() {
    const counters = document.querySelectorAll('.stat-counter');

    counters.forEach(counter => {
        const target = parseInt(counter.getAttribute('data-target'));
        const increment = target / 100;
        let current = 0;

        const timer = setInterval(() => {
            current += increment;
            if (current >= target) {
                current = target;
                clearInterval(timer);
            }
            counter.textContent = Math.floor(current).toLocaleString();
        }, 20);
    });
}

// Password strength checker
function checkPasswordStrength(password) {
    const strength = {
        0: "Very Weak",
        1: "Weak",
        2: "Fair",
        3: "Good",
        4: "Strong"
    };

    let score = 0;

    // Check length
    if (password.length >= 8) score++;

    // Check for lowercase
    if (/[a-z]/.test(password)) score++;

    // Check for uppercase
    if (/[A-Z]/.test(password)) score++;

    // Check for numbers
    if (/[0-9]/.test(password)) score++;

    // Check for special characters
    if (/[^A-Za-z0-9]/.test(password)) score++;

    return {
        score: score,
        text: strength[score]
    };
}

// Donation type switcher
function toggleDonationFields() {
    const donationType = document.getElementById('donationType').value;
    const itemDescriptionGroup = document.getElementById('itemDescriptionGroup');
    const quantityGroup = document.getElementById('quantityGroup');
    const monetaryValueGroup = document.getElementById('monetaryValueGroup');

    if (donationType === 'Money') {
        itemDescriptionGroup.style.display = 'none';
        quantityGroup.style.display = 'none';
        monetaryValueGroup.style.display = 'block';
    } else {
        itemDescriptionGroup.style.display = 'block';
        quantityGroup.style.display = 'block';
        monetaryValueGroup.style.display = 'none';
    }
}

// Search functionality
function filterTable(tableId, searchId) {
    const search = document.getElementById(searchId).value.toLowerCase();
    const table = document.getElementById(tableId);
    const rows = table.getElementsByTagName('tr');

    for (let i = 1; i < rows.length; i++) {
        const cells = rows[i].getElementsByTagName('td');
        let found = false;

        for (let j = 0; j < cells.length; j++) {
            const cellText = cells[j].textContent.toLowerCase();
            if (cellText.includes(search)) {
                found = true;
                break;
            }
        }

        rows[i].style.display = found ? '' : 'none';
    }
}

// Date validation
function validateDateRange(startDateId, endDateId) {
    const startDate = new Date(document.getElementById(startDateId).value);
    const endDate = new Date(document.getElementById(endDateId).value);

    if (startDate > endDate) {
        alert('End date cannot be before start date');
        return false;
    }

    return true;
}

// Character counter for textareas
function setupCharacterCounters() {
    const textareas = document.querySelectorAll('textarea[data-max-length]');

    textareas.forEach(textarea => {
        const maxLength = textarea.getAttribute('data-max-length');
        const counter = document.createElement('div');
        counter.className = 'form-text text-muted character-counter';
        counter.textContent = `0/${maxLength} characters`;

        textarea.parentNode.appendChild(counter);

        textarea.addEventListener('input', function () {
            const currentLength = this.value.length;
            counter.textContent = `${currentLength}/${maxLength} characters`;

            if (currentLength > maxLength) {
                counter.classList.add('text-danger');
            } else {
                counter.classList.remove('text-danger');
            }
        });
    });
}

// Initialize when document is ready
document.addEventListener('DOMContentLoaded', function () {
    setupCharacterCounters();

    // Initialize tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    const tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Initialize popovers
    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    const popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
});