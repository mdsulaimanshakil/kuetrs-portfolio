document.addEventListener('DOMContentLoaded', () => {
    // ─── Mobile Menu Toggle ──────────────────────────────
    const hamburger = document.getElementById('hamburger');
    const navLinks = document.querySelector('.nav-links');
    const navItems = document.querySelectorAll('.nav-link');

    if (hamburger && navLinks) {
        hamburger.addEventListener('click', () => {
            hamburger.classList.toggle('active');
            navLinks.classList.toggle('active');
        });
        navItems.forEach(item => {
            item.addEventListener('click', () => {
                hamburger.classList.remove('active');
                navLinks.classList.remove('active');
            });
        });
    }

    // ─── Dark / Light Mode Toggle ────────────────────────
    const themeToggle = document.getElementById('theme-toggle');
    const body = document.body;

    const savedTheme = localStorage.getItem('kuetrs-theme');
    if (savedTheme === 'light') body.classList.add('light-mode');

    if (themeToggle) {
        themeToggle.addEventListener('click', () => {
            body.classList.toggle('light-mode');
            localStorage.setItem('kuetrs-theme', body.classList.contains('light-mode') ? 'light' : 'dark');
        });
    }

    // ─── Active Nav Link on Scroll ───────────────────────
    const sections = document.querySelectorAll('section[id]');
    function highlightActiveLink() {
        let current = '';
        sections.forEach(section => {
            if (window.scrollY >= section.offsetTop - 150) {
                current = section.getAttribute('id');
            }
        });
        navItems.forEach(item => {
            item.classList.remove('active');
            const href = item.getAttribute('href');
            if (href === `#${current}` || href === `/#${current}`) {
                item.classList.add('active');
            }
        });
    }
    window.addEventListener('scroll', highlightActiveLink, { passive: true });
    highlightActiveLink();

    // ─── Scroll Reveal Animations ────────────────────────
    const revealElements = document.querySelectorAll('.reveal');
    const revealObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('active');
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.15, rootMargin: '0px 0px -50px 0px' });

    revealElements.forEach(el => revealObserver.observe(el));
});
