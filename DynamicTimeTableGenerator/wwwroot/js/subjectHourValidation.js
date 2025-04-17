document.addEventListener("DOMContentLoaded", function () {
    const hourInputs = document.querySelectorAll(".subject-hour");
    const generateBtn = document.querySelector("#generateBtn");
    const validationMessage = document.querySelector("#validationMessage");

    function validate() {
        let total = 0;
        hourInputs.forEach(input => {
            let val = parseInt(input.value);
            if (!isNaN(val)) total += val;
        });

        if (total === totalHours) {
            validationMessage.textContent = "";
            generateBtn.disabled = false;
        } else {
            validationMessage.textContent = `Total hours must equal ${totalHours}. Current total: ${total}`;
            generateBtn.disabled = true;
        }
    }

    hourInputs.forEach(input => {
        input.addEventListener("input", validate);
    });
});
