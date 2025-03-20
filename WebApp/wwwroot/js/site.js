document.addEventListener('DOMContentLoaded', function () {

    //Open modal
    const modalButtons = document.querySelectorAll('[data-modal="true"]');

    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal) 
                modal.style.display = 'flex';
        });
    });

    //Close modal
    const closeButtons = document.querySelectorAll('[data-close="true"]');
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal');
            if (modal) { 
                modal.style.display = 'none';
                //clear form data
            }
        });
    });
})
