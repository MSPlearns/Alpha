//---------dropdowns
const dropdownButtons = document.querySelectorAll('[data-type="dropdown"]');

dropdownButtons.forEach(button => {
    button.addEventListener('click', function (e) {
        e.stopPropagation();

        const targetSelector = button.getAttribute('data-target');
        let targetElement;

        // If it's an ID selector (starts with #), select globally
        if (targetSelector.startsWith('#')) {
            targetElement = document.querySelector(targetSelector);
        } else {
            // Otherwise, look for it within the closest card
            const card = button.closest('.card');
            targetElement = card ? card.querySelector(targetSelector) : null;
        }

        // Close all other open dropdowns
        document.querySelectorAll('.dropdown.show').forEach(openDropdown => {
            if (openDropdown !== targetElement) {
                openDropdown.classList.remove('show');
            }
        });

        // Toggle the target dropdown
        if (targetElement) {
            targetElement.classList.toggle('show');
        }
    });
});

// Prevent dropdowns from closing when clicking inside them
document.querySelectorAll('.dropdown').forEach(dropdown => {
    dropdown.addEventListener('click', e => e.stopPropagation());
});

// Close all dropdowns on outside click
document.addEventListener('click', () => {
    document.querySelectorAll('.dropdown.show').forEach(dropdown => {
        dropdown.classList.remove('show');
    });
});



//---------modals

// Open  modals
const modalButtons = document.querySelectorAll('[data-type = "modal"]');
modalButtons.forEach(button => {
    button.addEventListener('click', function (e) {
        e.stopPropagation();
        const targetSelector = button.getAttribute('data-target');
        const targetElement = document.querySelector(targetSelector);
        if (targetElement) {
            setTimeout(() => {
                initializeQuillEditors();
            }, 50); // Delay just enough for DOM insertion
            targetElement.classList.toggle('show-modal');
        }
    })
});
// Close modals
const closeButtons = document.querySelectorAll('[data-type = "close"]');
closeButtons.forEach(button => {
    button.addEventListener('click', function (e) {
        const targetSelector = button.getAttribute('data-target');
        const targetElement = document.querySelector(targetSelector);
        if (targetElement) {
            targetElement.classList.toggle('show-modal');
        }
    })
});


//----------form select input
document.querySelectorAll(".form-select").forEach((select) => {
    const trigger = select.querySelector(".form-select-trigger");
    const triggerText = trigger.querySelector(".form-select-text");
    const options = select.querySelectorAll(".form-select-option");
    const hiddenInput = select.querySelector('input[type="hidden"]');
    const placeholder = select.dataset.placeholder || "Choose";

    const setValue = (value = "", text = placeholder) => {
        triggerText.textContent = text;
        hiddenInput.value = value;
        select.classList.toggle("has-placeholder", !value);
    };
    setValue();

    trigger.addEventListener("click", (e) => {
        e.stopPropagation();
        document
            .querySelectorAll(".form-select.open")
            .forEach((el) => el !== select && el.classList.remove("open"));
        select.classList.toggle("open");
    });

    options.forEach((option) =>
        option.addEventListener("click", () => {
            setValue(option.dataset.value, option.textContent);
            select.classList.remove("open");
        })
    );

    document.addEventListener("click", (e) => {
        if (!select.contains(e.target)) {
            select.classList.remove("open");
        }
    });
});

//----------tooltips
const floatingTooltips = document.querySelectorAll('[data-type="tooltip"]');
floatingTooltips.forEach(tooltip => {
    tooltip.addEventListener('mouseenter', function (e) {
        const targetSelector = tooltip.getAttribute('data-target');
        const targetElement = tooltip.querySelector(targetSelector);
        if (targetElement) {
            targetElement.classList.remove('hide');
        }
    });
    tooltip.addEventListener('mouseleave', function (e) {
        const targetSelector = tooltip.getAttribute('data-target');
        const targetElement = tooltip.querySelector(targetSelector);
        if (targetElement) {
            targetElement.classList.add('hide');
        }
    });
});

//-------toogle dark mode

const toggle = document.getElementById('darkModeToggle');
const htmlElement = document.documentElement;

const savedTheme = localStorage.getItem('theme');
if (savedTheme) {
    htmlElement.setAttribute('data-theme', savedTheme);
    toggle.checked = savedTheme === 'dark';
}

toggle.addEventListener('change', function () {
    const newTheme = this.checked ? 'dark' : 'light';
    htmlElement.setAttribute('data-theme', newTheme);
    localStorage.setItem('theme', newTheme);
});

//---------Quill editors

// WYSIWYG editor function to be called when opening a modal
//This was generated by cGPT because i could not get ALL the quills to work at the same time.
function initializeQuillEditors() {

    const editors = [
        { id: 'add-project-description-textarea', editorId: 'add-project-description-wysiwyg-editor', toolbarId: 'add-project-description-wysiwyg-toolbar', placeholder: 'Write a description...' },
        { id: 'edit-project-description-textarea', editorId: 'edit-project-description-wysiwyg-editor', toolbarId: 'edit-project-description-wysiwyg-toolbar', placeholder: 'The current description will be kept (click to edit)...' },
        { id: 'message-body-textarea', editorId: 'message-wysiwyg-editor', toolbarId: 'message-wysiwyg-toolbar', placeholder: 'Write your message...' },
        { id: 'add-status-description-textarea', editorId: 'add-status-description-wysiwyg-editor', toolbarId: 'add-status-description-wysiwyg-toolbar', placeholder: 'Write a description...' },
        { id: 'edit-status-description-textarea', editorId: 'edit-status-description-wysiwyg-editor', toolbarId: 'edit-status-description-wysiwyg-toolbar', placeholder: 'The current description will be kept (click to edit)...' },
    ];

    editors.forEach(({ id, editorId, toolbarId, placeholder }) => {

        const textarea = document.getElementById(id);
        const editorEl = document.getElementById(editorId);
        const toolbarEl = document.getElementById(toolbarId);


        if (textarea && editorEl && toolbarEl && !editorEl.dataset.initialized) {


            const quill = new Quill(`#${editorId}`, {
                modules: {
                    syntax: true,
                    toolbar: `#${toolbarId}`
                },
                theme: 'snow',
                placeholder
            });

            quill.on('text-change', function () {
                textarea.value = quill.root.innerHTML;
            });

            editorEl.dataset.initialized = "true";
        }
    });

}