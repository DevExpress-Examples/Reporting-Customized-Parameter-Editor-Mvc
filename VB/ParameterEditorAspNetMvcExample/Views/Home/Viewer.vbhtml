<script type="text/html" id="employeeID-custom-editor">
    <div data-bind="dxTreeList: { dataSource: data, columns: columns, showBorders: true,
         selection:{mode: 'single'}, onSelectionChanged: onSelectionChanged }"> </div>
</script>

<script type="text/javascript">
    // Specifies the endpoint for the tree list data.
    var data = window.location.protocol + "//" + window.location.host + "/Home/ListEmployees";
    // Specifies the columns to display in the tree list.
    var columns = [{ dataField: "name", caption: "Name" }, { dataField: "title", caption: "Title" }];

    var p_employeeID_editor;

    function customizeParameterEditors(s, e) {
        if (e.parameter.type === 'System.DateTime') {
            e.info.editor.extendedOptions = { type: 'date' };
            var validationRule = {
                type: "range",
                min: new Date(2000, 0, 1),
                message: "We do not retain data prior to the year 2000."
            };
            e.info.validationRules.push(validationRule);
        };
        if (e.parameter.name == "p_employeeID") {
            e.info.editor = { header: 'employeeID-custom-editor' };
        };
    };
    function onSelectionChanged(s, e) {
        p_employeeID_editor = s;
        if (s.selectedRowsData.length > 0) {
            var selectedEmployeeID = s.selectedRowsData[0].id;
            WebDocumentViewer1.GetParametersModel().p_employeeID(selectedEmployeeID);
        }
    };
    function onParametersReset(s, e) {
        p_employeeID_editor.component.deselectAll();
    };

    function setPanelWidth(s, e) {
        e.tabPanel.width(500);
    };
</script>

@Html.DevExpress().WebDocumentViewer(
                Sub(settings)
                    settings.Name = "WebDocumentViewer1"
                    settings.ClientSideEvents.BeforeRender = "setPanelWidth"
                    settings.ClientSideEvents.CustomizeParameterEditors = "customizeParameterEditors"
                    settings.ClientSideEvents.ParametersReset = "onParametersReset"
                End Sub).Bind("TestReport").GetHtml()
