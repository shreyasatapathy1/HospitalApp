function updateAppointmentsNotificationCount() {

    $.ajax({
        url: "https://localhost:7263/api/Appointments/count/confirmed", // API URL for pending count
        type: "GET",
        success: function (response) {
            console.log(response)
            // Assuming your API returns { success: true, count: X }
            if (response.success) {
                const count = response.count;

                // Update the badge
                const badge = $(".icon-button__badge");
                badge.text(count);

                // Show or hide the badge based on count
                if (count === 0) {
                    badge.addClass("visually-hidden");
                } else {
                    badge.removeClass("visually-hidden");
                }
            }
        },
        error: function () {
            console.error("Failed to fetch Appointment notification count.");
        }
    });
}

// Update the badge every 10 seconds
setInterval(updateAppointmentsNotificationCount(), 10000);

// Initial call to load the count
updateAppointmentsNotificationCount();