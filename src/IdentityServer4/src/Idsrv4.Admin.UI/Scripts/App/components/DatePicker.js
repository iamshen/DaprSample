﻿$(function() {

    var adminDatePicker = {
        getCookie: function getCookie(cname) {
            const name = cname + "=";
            const ca = document.cookie.split(";");
            for (let i = 0; i < ca.length; i++) {
                let c = ca[i];
                while (c.charAt(0) === " ") {
                    c = c.substring(1);
                }
                if (c.indexOf(name) === 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        },
        getUiCultureFromAspNetCoreCultureCookie: function() {
            const cookieValue = decodeURIComponent(this.getCookie(".AspNetCore.Culture"));
            if (cookieValue === null || cookieValue === "") return null;

            const parameters = cookieValue.split("|");
            for (let i = 0; i < parameters.length; i++) {
                if (parameters[i].indexOf("=") !== -1) {
                    const p = parameters[i].split("=");
                    if (p[0] === "uic") {
                        return p[1];
                    }
                }
            }
            return null;
        },
        getLanguage: function() {
            //Defaults to en if no UiCulture found.
            var language = adminDatePicker.getUiCultureFromAspNetCoreCultureCookie() || "en";
            // bootstrap DatePicker supports Taiwanese chinese as well as Mainland. 
            // Defaults to Mainland as we currently have no way of specifying variants.
            if (language === "zh") language = "zh-CN";
            return language;
        },
        initDatePickers: function() {
            $(".datepicker").datepicker({
                autoclose: true,
                todayHighlight: true,
                language: adminDatePicker.getLanguage()
            });
        }

    };


    adminDatePicker.initDatePickers();
})