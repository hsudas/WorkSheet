$(document).ready(function () {

    

    getTask = function () {
        $.ajax("/Task/GetTaskList", {
            success: function (data) {
                taskList.removeAll();
                for (var i = 0; i < data.length; i++) {
                    var d = new Date(parseInt((data[i]["EventDate"]).slice(6, -2)));
                    d = '' + (1 + d.getMonth()) + '/' + d.getDate() + '/' + d.getFullYear().toString();
                    data[i]["EventDate"] = d;
                    //console.log(data[i]);
                    //taskList.push(data[i]);
                }
            }
        })
    };


    deleteTask = function (data) {
        $.ajax("/Task/Delete/" + data.TaskId, {
            type: "POST",
            success: function () {
                taskList.remove(data);
            }
        })
    };

    var TaskViewModel = function () {

        this.selectedJob = ko.observable();

    };

    ko.applyBindings(new TaskViewModel());
});
