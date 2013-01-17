function begin() {
  App.rabbits = ko.observableArray();
  App.selectedRabbit = ko.observable();
  App.tasks = ko.observableArray();
  App.canAddTask = ko.observable(false);
  App.isLoading = ko.observable(false);
  App.addTask = function() {
    App.tasks.unshift(TaskViewModel().validate());
  };
  App.selectedRabbit.subscribe(loadTasks);
  ko.applyBindings(App, $("#dashboard").element);
  getRabbits();
}

function getRabbits() {
  $.getJSON(App.routes.GetRabbitsUrl, function(data) {
    App.rabbits(data.Rabbits);
    App.rabbits.CreateRabbitUrl = data.CreateRabbitUrl;
  });
}

function loadTasks(rabbit) {
  $.getJSON(rabbit.TasksUrl, function(data) {
    App.tasks(ToTaskViewModels(data.Tasks));
    App.tasks.CreateTaskUrl = data.CreateTaskUrl;
    App.canAddTask(true);
  });
}

function TaskViewModel(task) {
  if(!task) {
    task = { };
    task.RabbitId = App.selectedRabbit().Id;
    task.Description = "";
    task.DueDate = "";
    task.SaveUrl = App.tasks.CreateTaskUrl;
    task.DeleteUrl = null;
  }

  var vm = {
    RabbitId: ko.observable(task.RabbitId),
    Description: ko.observable(task.Description),
    DueDate: ko.observable(task.DueDate),
    SaveUrl: ko.observable(task.SaveUrl),
    DeleteUrl: ko.observable(task.DeleteUrl),
    CanSave: ko.observable(false),
    ErrorsString: ko.observable(""),
    IsInvalid: ko.observable(false),
    ParsedDate: ko.observable("")
  };

  if(task.Id) {
    vm.Id = ko.observable(task.Id);
  }

  vm.parseDate = function() {
    var date = Date.parse(vm.DueDate());

    if(date) {
      vm.ParsedDate(date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear());
      return;
    }

    vm.ParsedDate("?");
  };

  vm.validate = function() {
    var errors = vm.errors();
    if(errors) {
      vm.ErrorsString(errors.join(", "));
      vm.CanSave(false);
      vm.IsInvalid(true);
    } else {
      vm.ErrorsString("");
      vm.CanSave(true);
      vm.IsInvalid(false);
    }

    return vm;
  };

  vm.save = function() {
    vm.DueDate(vm.ParsedDate());
    $.post(vm.SaveUrl(), JSON.parse(ko.toJSON(vm)), function() {
      vm.CanSave(false);
    });
  };

  vm.errors = function() {
    var result = [];

    if(!Date.parse(vm.DueDate())) {
      result.push("invalid date");
    }

    if(!vm.Description()) {
      result.push("description required");
    }

    if(result.length > 0) return result;

    else return null;
  };

  vm.determineSave = function(data, e) {
    if(e.keyCode == 13) {
      vm.save();
    }
  };

  vm.destroy = function(data) {
    if(vm.Id) {
      $.post(vm.DeleteUrl(), function() {
        App.tasks.remove(vm);
      });
    } else {
      App.tasks.remove(vm);
    }
  };

  vm.Description.subscribe(vm.validate);
  vm.DueDate.subscribe(vm.validate);
  vm.DueDate.subscribe(vm.parseDate);
  vm.parseDate();

  return vm;
}

function ToTaskViewModels(tasks) {
  var results = [];
  _.each(tasks, function(task) {
    results.push(TaskViewModel(task));
  });

  return results;
}

$(function () {
  window.App = { };
  $.getJSON('/api', function(d) {
    window.App.routes = d;
    begin();
  });
});
