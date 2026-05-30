document.addEventListener('DOMContentLoaded', () => {
    const html = document.getElementById('htmlRoot');
    const toggle = document.getElementById('darkModeToggle');
    const icon = document.getElementById('darkModeIcon');

    function applyTheme(theme) {
        html.setAttribute('data-bs-theme', theme);
        localStorage.setItem('theme', theme);
        if (icon) {
            icon.className = theme === 'dark' ? 'bi bi-sun-fill' : 'bi bi-moon-stars-fill';
        }
    }

    // Sync icon with stored theme on load
    const stored = localStorage.getItem('theme') || 'light';
    applyTheme(stored);

    toggle?.addEventListener('click', () => {
        const current = html.getAttribute('data-bs-theme');
        applyTheme(current === 'dark' ? 'light' : 'dark');
    });

    // Auto-dismiss success alerts after 4 seconds
    setTimeout(() => {
        document.querySelectorAll('.alert-success').forEach(el => {
            bootstrap.Alert.getOrCreateInstance(el).close();
        });
    }, 4000);
});
