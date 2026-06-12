// ─── Admin JS ─────────────────────────────────────────────

document.addEventListener('DOMContentLoaded', () => {

    // ─── Sidebar Toggle ──────────────────────────────────
    const sidebar = document.getElementById('admin-sidebar');
    const main = document.getElementById('admin-main');
    const toggleBtn = document.getElementById('sidebar-toggle');

    if (toggleBtn && sidebar && main) {
        toggleBtn.addEventListener('click', () => {
            sidebar.classList.toggle('collapsed');
            main.classList.toggle('expanded');
            localStorage.setItem('sidebar-collapsed', sidebar.classList.contains('collapsed'));
        });
        // Restore state
        if (localStorage.getItem('sidebar-collapsed') === 'true') {
            sidebar.classList.add('collapsed');
            main.classList.add('expanded');
        }
    }

    // ─── DataTables ───────────────────────────────────────
    if (typeof $.fn.DataTable !== 'undefined') {
        $('.datatable').DataTable({
            pageLength: 25,
            order: [],
            language: {
                search: '',
                searchPlaceholder: 'Search...',
                lengthMenu: 'Show _MENU_ entries'
            },
            dom: '<"d-flex justify-content-between align-items-center mb-3"lf>rtip'
        });
    }

    // ─── SweetAlert Delete Confirmation ──────────────────
    document.querySelectorAll('.btn-delete').forEach(btn => {
        btn.addEventListener('click', async (e) => {
            e.preventDefault();

            const id = btn.dataset.id;
            const url = btn.dataset.url;
            const name = btn.dataset.name || 'this item';

            const result = await Swal.fire({
                title: 'Are you sure?',
                html: `Delete <strong>${name}</strong>?<br><small>This action cannot be undone.</small>`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'Cancel',
                reverseButtons: true
            });

            if (!result.isConfirmed) return;

            try {
                // Get anti-forgery token
                const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value
                    || document.querySelector('meta[name="csrf-token"]')?.content;

                const formData = new FormData();
                formData.append('id', id);
                if (token) formData.append('__RequestVerificationToken', token);

                const res = await fetch(url, { method: 'POST', body: formData });
                const data = await res.json();

                if (data.success) {
                    Swal.fire({ title: 'Deleted!', text: `${name} has been deleted.`, icon: 'success', timer: 1500, showConfirmButton: false });
                    // Remove row from DataTable or table
                    const row = btn.closest('tr') || btn.closest('.admin-gallery-item');
                    if (row) row.remove();
                    // If on detail page, redirect back
                    else setTimeout(() => window.history.back(), 1600);
                } else {
                    Swal.fire('Error', 'Could not delete. Please try again.', 'error');
                }
            } catch {
                Swal.fire('Error', 'Network error. Please try again.', 'error');
            }
        });
    });

    // ─── Auto-dismiss Alerts ─────────────────────────────
    document.querySelectorAll('.alert').forEach(alert => {
        setTimeout(() => {
            const bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
            if (bsAlert) bsAlert.close();
        }, 5000);
    });

});
