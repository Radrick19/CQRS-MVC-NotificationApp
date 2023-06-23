document.addEventListener("DOMContentLoaded", function () {
	let logoutPanelButton : HTMLElement = document.querySelector('.logout-panel-button');
	let logoutPanel : HTMLElement = document.querySelector('.logout-panel');
	let IsPanelOpen = false;
	logoutPanelButton.addEventListener('click', function () {
		if (IsPanelOpen) {
			logoutPanel.style.display = 'none';
			logoutPanelButton.style.backgroundColor = null;
			IsPanelOpen = false;
		}
		else {
			logoutPanel.style.display = 'flex';
			logoutPanelButton.style.backgroundColor = '#dbdbdb';
			IsPanelOpen = true;
		}
	})
})