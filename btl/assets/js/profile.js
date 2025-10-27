document.addEventListener('DOMContentLoaded', function () {
    const profileNavItems = document.querySelectorAll('.profile-nav-item:not(.logout)');
    const tabContents = document.querySelectorAll('.profile-content .tab-content');

    function showTab(tabId) {
        tabContents.forEach(content => {
            content.classList.remove('active');
            content.setAttribute('aria-hidden', 'true');
        });
        profileNavItems.forEach(item => {
            item.classList.remove('active');
            item.setAttribute('aria-selected', 'false');
        });

        const activeTabContent = document.getElementById(tabId + '-tab');
        const activeNavItem = document.querySelector(`.profile-nav-item[data-tab="${tabId}"]`);

        if (activeTabContent && activeNavItem) {
            activeTabContent.classList.add('active');
            activeTabContent.setAttribute('aria-hidden', 'false');
            activeNavItem.classList.add('active');
            activeNavItem.setAttribute('aria-selected', 'true');

            if (history.pushState) {
                history.pushState(null, null, '#' + tabId);
            } else {
                window.location.hash = '#' + tabId;
            }
        } else {
            if (tabContents[0] && profileNavItems[0]) {
                tabContents[0].classList.add('active');
                tabContents[0].setAttribute('aria-hidden', 'false');
                profileNavItems[0].classList.add('active');
                profileNavItems[0].setAttribute('aria-selected', 'true');
            }
        }
    }

    profileNavItems.forEach(item => {
        item.addEventListener('click', function (event) {
            event.preventDefault(); // Ngăn hành vi nhảy trang mặc định của thẻ <a>
            const tabId = this.getAttribute('data-tab');
            if (tabId) {
                showTab(tabId);
            }
        });
    });

    const initialTab = window.location.hash.substring(1); // Lấy phần sau dấu #
    if (initialTab) {
        showTab(initialTab);
    } else {
        showTab('info');
    }
});