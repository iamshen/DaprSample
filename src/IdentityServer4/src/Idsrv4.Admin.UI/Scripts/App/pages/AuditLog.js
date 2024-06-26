﻿var auditLog = {
    createJsonTree: function(json) {
        var result;

        try {
            result = JSONTree.create(JSON.parse(json));
        } catch (e) {
            result = JSONTree.create(json);
        }

        return result;
    },

    initJsonTrees: function() {
        $(".json-tree").each(function() {

            const json = $(this).data("json-tree");
            const result = auditLog.createJsonTree(json);

            $(this).html(result);
        });
    },

    eventHandlers: function() {
        $(".audit-subject-button").click(function() {
            const subjectId = $(this).data("subject-identifier");
            const subjectName = $(this).data("subject-name");
            const subjectType = $(this).data("subject-type");
            const json = $(this).data("subject-additional-data");

            $(".modal-title").html(subjectName + " - " + subjectId + " - " + "(" + subjectType + ")");
            $(".audit-modal-value").html(auditLog.createJsonTree(json));
            $(".audit-modal").modal("show");
        });

        $(".audit-action-button").click(function() {
            const json = $(this).data("action");
            const actionTitle = $(this).data("action-title");

            $(".modal-title").html(actionTitle);
            $(".audit-modal-value").html(auditLog.createJsonTree(json));
            $(".audit-modal").modal("show");
        });

        $(".audit-log-delete-button").click(function() {

            $(".audit-log-form").validate();

            if ($(".audit-log-form").validate().form()) {

                $("#deleteLogsModal").modal("show");
                return false;
            } else {
                $(this).submit();
                return false;
            }
        });
    },

    init: function() {

        $(function() {
            auditLog.eventHandlers();
            auditLog.initJsonTrees();
        });

    }
};

auditLog.init();