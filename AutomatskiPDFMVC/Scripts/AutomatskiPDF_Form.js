
$().ready(function () {

    jQuery.validator.addMethod("isDate", function (value, element) {
        var dateRegex = /^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-.\/])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;
        return this.optional(element) || dateRegex.test($("#DokumentPodaci_DatumIzrade").val());
    }, "Upišite ispravan datum");

    $('#form1').submit(function (evt) {
        if ($('#form1').valid()) {
            blockUIForDownload();
        }
        else {
            evt.preventDefault();
        }
        //evt.preventDefault();
    });

    $(function () {
        DefineValidationRulesKontrolaPovezanosti();

        ko.bindingHandlers.dump = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var context = valueAccessor();
                var allBindings = allBindingsAccessor();
                var pre = document.createElement('pre');

                element.appendChild(pre);

                var dumpJSON = ko.computed({
                    read: function () {
                        return ko.toJSON(context, null, 2);
                    },
                    disposeWhenNodeIsRemoved: element
                });

                ko.applyBindingsToNode(pre,
                    { text: dumpJSON }
                );
            }
        };

        (function () {
            var existing = ko.bindingProvider.instance;

            ko.bindingProvider.instance = {
                nodeHasBindings: existing.nodeHasBindings,
                getBindings: function (node, bindingContext) {
                    var bindings;
                    try {
                        bindings = existing.getBindings(node, bindingContext);
                    }
                    catch (ex) {
                        if (window.console && console.log) {
                            console.log("binding error", ex.message, node, bindingContext);
                        }
                    }

                    return bindings;
                }
            };

        })();

        ko.applyBindings(new KontrolaPovezanostiViewModel(), document.getElementById("podaciKontrolaPovezanosti"));

        $("#form1").validate({
            rules: {
                "OsnivacObrt.Ime": "required",
                "OsnivacObrt.Prezime": "required",
                "OsnivacObrt.OIB": {
                    required: true,
                    rangelength: [11, 11]
                },
                "OsnivacObrt.Email": {
                    required:true,
                    email: true
                },
                "OsnivacObrt.KontaktBroj": "required",
                "OsnivacObrt.Mjesto": "required",
                "OsnivacObrt.UlicaIKucniBroj": "required",
                "Obrt.Naziv": "required",
                "Obrt.Oznaka": "required",
                "Obrt.ImeObrtnika": "required",
                "Obrt.PrezimeObrtnika": "required",
                "Obrt.Mjesto": "required",
                "Obrt.UlicaIKucniBroj": "required",
                "OvlasteniZastupnik.Ime": "required",
                "OvlasteniZastupnik.Prezime": "required",
                "OvlasteniZastupnik.Oib": {
                    required: true,
                    rangelength: [11, 11]
                },
                "OvlasteniZastupnik.BrojURegistruHanfe": "required",
                "OvlasteniZastupnik.Mjesto": "required",
                "OvlasteniZastupnik.UlicaIKucniBroj": "required",
                "DokumentPodaci.MjestoIzrade": "required",
                "DokumentPodaci.DatumIzrade": {
                    required: true,
                    isDate: true
                },
                "EmailPodaci.EmailAdresaZaDostavu": {
                    required: true,
                    email: true
                }
            },
            messages: {
                "OsnivacObrt.Ime": "Unesite ime osnivača obrta.",
                "OsnivacObrt.Prezime": "Unesite prezime osnivača obrta.",
                "OsnivacObrt.OIB": {
                    required: "Unesite OIB osnivača obrta.",
                    rangelength: "Unesite ispravan OIB osnivača obrta."
                },
                "OsnivacObrt.Email": {
                    required: "Unesite email osnivača obrta",
                    email: "Unesite ispravan email osnivača obrta."
                },
                "OsnivacObrt.KontaktBroj": "Unesite kontakt broj osnivača obrta.",
                "OsnivacObrt.Mjesto": "Unesite mjesto osnivača obrta.",
                "OsnivacObrt.UlicaIKucniBroj": "Unesite ulicu i kućni broj osnivača obrta.",
                "Obrt.Naziv": "Unesite naziv obrta.",
                "Obrt.Oznaka": "Unesite oznaku obrta.",
                "Obrt.ImeObrtnika": "Unesite ime obrtnika.",
                "Obrt.PrezimeObrtnika": "Unesite prezime obrtnika.",
                "Obrt.Mjesto": "Unesite mjesto obrta.",
                "Obrt.UlicaIKucniBroj": "Unesite ulicu i kućni broj obrta.",
                "OvlasteniZastupnik.Ime": "Unesite ime ovlaštenog zastupnika.",
                "OvlasteniZastupnik.Prezime": "Unesite prezime ovlaštenog zastupnika.",
                "OvlasteniZastupnik.Oib": {
                    required: "Unesite OIB ovlaštenog zastupnika.",
                    rangelength: "Unesite ispravan OIB ovlaštenog zastupnika."
                },
                "OvlasteniZastupnik.BrojURegistruHanfe": "Unesite broj u registru Hanfe ovlaštenog zastupnika.",
                "OvlasteniZastupnik.Mjesto": "Unesite mjesto ovlaštenog zastupnika.",
                "OvlasteniZastupnik.UlicaIKucniBroj": "Unesite ulicu i kućni broj ovlaštenog zastupnika.",
                "DokumentPodaci.MjestoIzrade": "Upišite mjesto izrade",
                "DokumentPodaci.DatumIzrade": {
                    required: "Upišite datum izrade"
                },
                "EmailPodaci.EmailAdresaZaDostavu": {
                    required: "Upišite email adresu za dostavu.",
                    email: "Unesite ispravnu email adresu za dostavu."
                }
            }
        });
    });

    var ovlasteniZastupnik = $("input[name='OvlasteniZastupnik.KontrolaOvlastenja']");
    var initial = ovlasteniZastupnik.is(":checked");
    var ovlasteniZastupnikPolja = $("#podaciOvlasteniZastupnik")[initial ? "removeClass" : "addClass"]("hide");

    ovlasteniZastupnik.click(function () {
        ovlasteniZastupnikPolja[this.checked ? "removeClass" : "addClass"]("hide");
    });

    var kontrolaPovezanosti = $("input[name='KontrolaPovezanosti.KontrolaPovlastenosti']");
    var initialKontrolaPovezanosti = kontrolaPovezanosti.is(":checked");
    var kontrolaPovezanostiPolja = $("#podaciKontrolaPovezanosti")[initialKontrolaPovezanosti ? "removeClass" : "addClass"]("hide");

    kontrolaPovezanosti.click(function () {
        if ($("input[name='KontrolaPovezanosti.KontrolaPovlastenosti']").prop("checked")) {
            $("#podaciKontrolaPovezanosti").removeClass("hide");
        }
        else {  
            // hajdaj podaciKontrolaPovezanosti
            $("#podaciKontrolaPovezanosti").addClass("hide");
        }
    });

    if ($('#EmailPodaci_PosaljiNaEmail').prop('checked')) {
        $("#EmailPodaci_EmailAdresaZaDostavu").prop("disabled", false);
    }
    else {
        $("#EmailPodaci_EmailAdresaZaDostavu").prop("disabled", true);
    }

    $('#EmailPodaci_PosaljiNaEmail').click(function () {
        if ($('#EmailPodaci_PosaljiNaEmail').prop('checked')) {
            $("#EmailPodaci_EmailAdresaZaDostavu").prop("disabled", false);
        }
        else {
            $("#EmailPodaci_EmailAdresaZaDostavu").prop("disabled", true);
        }
    });
});

var fileDownloadCheckTimer;
function blockUIForDownload() {
    var token = new Date().getTime(); //use the current timestamp as the token value
    $('#download_token_value').val(token);
    $.blockUI({ message: 'Molim pričekajte...' });
    fileDownloadCheckTimer = window.setInterval(function () {
        var cookieValue = $.cookie('fileDownloadToken');
        if (cookieValue == token)
            finishDownload();
    }, 1000);
}

function finishDownload() {
    window.clearInterval(fileDownloadCheckTimer);
    $.cookie('fileDownloadToken', null); //clears this cookie value
    // clear errors
    // clear errors
    var container = $('#form1').find('[data-valmsg-summary="true"]');
    var list = container.find('ul');

    if (list && list.length) {
        list.empty();
        container.addClass('validation-summary-valid').removeClass('validation-summary-errors');
    }
    $.unblockUI();
}

var form = document.getElementById('form1');
form.elements.OsnivacObrt_Ime.onblur = function () {
    var form = this.form;
    form.elements.Obrt_ImeObrtnika.value = form.elements.OsnivacObrt_Ime.value;
};
form.elements.OsnivacObrt_Prezime.onblur = function () {
    var form = this.form;
    form.elements.Obrt_PrezimeObrtnika.value = form.elements.OsnivacObrt_Prezime.value;
};

$("#DokumentPodaci_DatumIzrade").datepicker({
        closeText: 'Zatvori',
        prevText: 'Prethodni mjesec',
        nextText: 'Slijedeći mjesec',
        currentText: 'Danas',
        monthNames: ['Siječanj', 'Veljača', 'Ožujak', 'Travanj', 'Svibanj', 'Lipanj',
            'Srpanj', 'Kolovoz', 'Rujan', 'Listopad', 'Studeni', 'Prosinac'],
        monthNamesShort: ['Sij.', 'Velj.', 'Ožu.', 'Tra.', 'Svi.', 'Lip.',
            'Srp.', 'Kol.', 'Ruj.', 'Lis.', 'Stu.', 'Pro.'],
        dayNames: ['Nedjelja', 'Ponedjeljak', 'Utorak', 'Srijeda', 'Četvrtak', 'Petak', 'Subota'],
        dayNamesShort: ['Ned.', 'Pon.', 'Uto.', 'Sri.', 'Čet.', 'Pet.', 'Sub.'],
        dayNamesMin: ['N', 'P', 'U', 'S', 'Č', 'P', 'S'],
        weekHeader: 'Tjedan',
        dateFormat: 'dd.mm.yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
});
$.datepicker.setDefaults($.datepicker.regional['hr']);
$("#DokumentPodaci_DatumIzrade").datepicker("setDate", new Date());

function FizickaOsobaViewModel() {
    var self = this;

    self.Ime = '';
    self.Prezime = '';
    self.OIB = '';
    self.Mjesto = '';
    self.UlicaIKucniBroj = '';
}

function PravnaOsobaViewModel() {
    var self = this;

    self.Naziv = '';
    self.OIB = '';
    self.Mjesto = '';
    self.UlicaIKucniBroj = '';
    self.DokazOUplacenojNaknadi = false;
}


function KontrolaPovezanostiViewModel() {
    var self = this;

    self.FizickeOsobe = ko.observableArray([
        { Ime: "", Prezime: "", OIB: "", Mjesto: "", UlicaIKucniBroj: "" }
    ]);

    self.AddFizickaOsoba = function () {
        self.FizickeOsobe.push(new FizickaOsobaViewModel());
    };

    self.RemoveFizickaOsoba = function (fizickaOsoba) {
        self.FizickeOsobe.pop(fizickaOsoba);
    }

    self.PravneOsobe = ko.observableArray();

    self.AddPravnaOsoba = function () {
        self.PravneOsobe.push(new PravnaOsobaViewModel());
    };

    self.RemovePravnaOsoba = function (pravnaOsoba) {
        self.PravneOsobe.pop(pravnaOsoba);
    }

    self.tipOsobe = [
        { tipOsobe: "Fizička" },
        { tipOsobe: "Pravna" },
    ];
}

function DefineValidationRulesKontrolaPovezanosti() {
    $.validator.addMethod("fizickaOsobaImeRequired", $.validator.methods.required, "Upišite ime fizičke osobe");
    $.validator.addMethod("fizickaOsobaPrezimeRequired", $.validator.methods.required, "Upišite prezime fizičke osobe");
    $.validator.addMethod("fizickaOsobaOIBRequired", $.validator.methods.required, "Upišite OIB fizičke osobe");
    $.validator.addMethod("fizickaOsobaOIBDuzina", $.validator.methods.rangelength, "Upišite ispravan OIB fizičke osobe");
    $.validator.addMethod("fizickaOsobaMjestoRequired", $.validator.methods.required, "Upišite mjesto fizičke osobe");
    $.validator.addMethod("fizickaOsobaUlicaIKucniBrojRequired", $.validator.methods.required, "Upišite ulicu i kućni broj fizičke osobe");

    $.validator.addClassRules("validate-kontrolapovezanosti-fizickaosoba-ime", { fizickaOsobaImeRequired: true });
    $.validator.addClassRules("validate-kontrolapovezanosti-fizickaosoba-prezime", { fizickaOsobaPrezimeRequired: true });
    $.validator.addClassRules("validate-kontrolapovezanosti-fizickaosoba-OIB", { fizickaOsobaOIBRequired: true, fizickaOsobaOIBDuzina: [11, 11] });
    $.validator.addClassRules("validate-kontrolapovezanosti-fizickaosoba-mjesto", { fizickaOsobaMjestoRequired: true });
    $.validator.addClassRules("validate-kontrolapovezanosti-fizickaosoba-ulicaikucnibroj", { fizickaOsobaUlicaIKucniBrojRequired: true });

    $.validator.addMethod("pravnaOsobaNazivRequired", $.validator.methods.required, "Upišite naziv pravne osobe");
    $.validator.addMethod("pravnaOsobaOIBRequired", $.validator.methods.required, "Upišite OIB pravne osobe");
    $.validator.addMethod("pravnaOsobaOIBDuzina", $.validator.methods.rangelength, "Upišite ispravan OIB pravne osobe");
    $.validator.addMethod("pravnaOsobaMjestoRequired", $.validator.methods.required, "Upišite mjesto pravne osobe");
    $.validator.addMethod("pravnaOsobaUlicaIKucniBrojRequired", $.validator.methods.required, "Upišite ulicu i kućni broj pravne osobe");
    $.validator.addMethod("pravnaOsobaDokazOUplacenojNaknadi", $.validator.methods.required, "Potvrdite dokaz o uplaćenoj naknadi");

    $.validator.addClassRules("validate-kontrolapovezanosti-pravnaosoba-naziv", { pravnaOsobaNazivRequired: true });
    $.validator.addClassRules("validate-kontrolapovezanosti-pravnaosoba-OIB", { pravnaOsobaOIBRequired: true, pravnaOsobaOIBDuzina: [11, 11] });
    $.validator.addClassRules("validate-kontrolapovezanosti-pravnaosoba-mjesto", { pravnaOsobaMjestoRequired: true });
    $.validator.addClassRules("validate-kontrolapovezanosti-pravnaosoba-ulicaikucnibroj", { pravnaOsobaUlicaIKucniBrojRequired: true });
    $.validator.addClassRules("validate-kontrolapovezanosti-pravnaosoba-dokazouplacenojnaknadi", { pravnaOsobaDokazOUplacenojNaknadi: true });
}

