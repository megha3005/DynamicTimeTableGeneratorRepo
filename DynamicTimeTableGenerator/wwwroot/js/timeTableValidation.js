document.addEventListener("DOMContentLoaded", function () {
    const workingDays = document.querySelector("#WorkingDays");
    const subjectsPerDay = document.querySelector("#SubjectsPerDay");
    const totalHours = document.querySelector("#totalHours");
    const submitBtn = document.querySelector("#submitBtn");

    function calculate() {
        let days = parseInt(workingDays.value);
        let subjects = parseInt(subjectsPerDay.value);
        if (!isNaN(days) && !isNaN(subjects)) {
            let total = days * subjects;
            totalHours.value = total;
            submitBtn.disabled = false;
        } else {
            totalHours.value = '';
            submitBtn.disabled = true;
        }
    }

    workingDays.addEventListener("input", calculate);
    subjectsPerDay.addEventListener("input", calculate);
});
