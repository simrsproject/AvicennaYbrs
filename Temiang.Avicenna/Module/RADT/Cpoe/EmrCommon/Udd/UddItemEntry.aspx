<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="UddItemEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.UddItemEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdUddItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <style>
            .AutoHeightGridClass .rgDataDiv {
                height: auto !important;
            }

            /* Big Checkbox */
            button.RadCheckBox {
                font-size: 20px;
            }

            /* makes the checkbox icon elastic in addition to the label text */
            .RadButton.RadCheckBox .rbIcon,
            .RadButton.RadCheckBox .rbIcon::before {
                font-size: inherit;
                width: 1em;
                height: 1em;
            }
        </style>
        <script type="text/javascript">
            var _stockStyleDisplay = '<%= AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionShowStock)?"block":"none" %>';
            function cboItemID_ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();

                var cboServ = $find("<%=cboServiceUnitID.ClientID %>");
                context["ServiceUnitID"] = cboServ.get_selectedItem().get_value();

                context["LocationID"] = document.getElementById("<%=hdnLocationID.ClientID%>").value

                context["IsOnlyInStock"] = <%= AppSession.Parameter.IsPrescriptionOnlyInStock.ToString().ToLower() %>;
                context["GuarantorID"] = "<%=GuarantorID%>";
                context["UserType"] = "<%=AppSession.UserLogin.SRUserType%>";

                context["IsRasproEnable"] = <%= (AppSession.Parameter.IsRasproEnable && RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.InPatient).ToString().ToLower() %>;
                context["AbLevel"] = document.getElementById("<%=hdnAbLevel.ClientID%>").value;
                context["AbRestrictionID"] = document.getElementById("<%=hdnAbRestrictionID.ClientID%>").value;

                //RasproSeqNo untuk filter antibiotik jika berdasar raspro sebelumnya (bukan new raspro form)
                if (document.getElementById("<%=hdnRasproIsNew.ClientID%>").value != "1")
                    context["RasproSeqNo"] = document.getElementById("<%=hdnRasproSeqNo4Filter.ClientID%>").value;
                else
                    context["RasproSeqNo"] = "0";

                context["RasproType"] = document.getElementById("<%=hdnRasproType.ClientID%>").value;
                context["RegistrationNo"] = "<%=RegistrationNo%>";
                context["IsFornas"] = <%=hdnFornas.Value.ToLower() %>;
                context["IsBalTot"] = false;
                context["IsUdd"] = true;
                context["IsFormPrescOrderForm"] = true;
                context["ArLine"] = "<%= AntibioticRestrictionForLine %>";
            }

            function cboItemID_ClientSelectedIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                __doPostBack(sender._uniqueId, "")
            }

            function cboConsumeUnit_ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();
                context["ItemID"] = $telerik.findControl(sender.get_parent().get_element(), "cboItemID").get_value();
            }

            function showDropDown(sender, eventArgs) {
                sender.showDropDown();
                sender.requestItems("[showall]", false);
            }
            function showDropDownNoKeypress(sender, eventArgs) {
                sender.showDropDown();
            }
            function cboMedicationConsume_ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();
                context["RefID"] = "MedicationConsume";
            }

            function cboEmbalace_ClientSelectedIndexChanged(sender, args) {
                var val = args.get_item().get_value();
                var combo = $find("<%= grdUddItem.ClientID %>_ctl00_ctl02_ctl02_EditFormControl_cboConsumeUnit");
                var itm = combo.findItemByValue(val);
                itm.select();
            }

            function cboConsumeMethod_ClientSelectedIndexChanged(sender, args) {
                var val = args.get_item().get_text();
                if (val.toLowerCase().includes("diketahui")) {
                    var txtConsumeQty = $find("<%= grdUddItem.ClientID %>_ctl00_ctl02_ctl02_EditFormControl_txtConsumeQty");
                    txtConsumeQty.set_value(1);
                }
            }

            function ApplyIsCompound(sender, args) {
                var tbl = document.getElementById('tblItemEntry');
                var isShow = args.get_checked();

                show_hide_column(tbl, 1, isShow);
            }

            function show_hide_column(tbl, colNo, isShow) {
                var rows = tbl.getElementsByTagName('tr');

                for (var row = 0; row < rows.length; row++) {
                    var cols = rows[row].children;
                    if (colNo >= 0 && colNo < cols.length) {
                        var cell = cols[colNo];
                        if (cell.tagName == 'TD') cell.style.display = isShow ? 'block' : 'none';
                    }
                }
            }

            function newRasal() {
                showRaspro("RASAL", "0", "new");
            }
            function newRaslan() {
                showRaspro("RASLAN", "0", "new");
            }
            function newRaspraja() {
                showRaspro("RASPRAJA", "0", "new");
            }
            function newRaspatur() {
                showRaspro("RASPATUR", "0", "new");
            }

            function showRaspro(rasprotype, seqno, mod) {
                switch (rasprotype) {
                    case "RASAL":
                    case "RASLAN":
                        openWinRaspro("<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproForm.aspx?mod=" + mod + "&rsno=<%=hdnRasproSeqNo.Value%>&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&seqno=" + seqno + "&raspro=" + rasprotype, "");
                        break;
                    case "RASPRAJA":
                        openWinRaspro("<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasprajaEntry.aspx?mod=" + mod +"&rsno=<%=hdnRasproSeqNo.Value%>&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&seqno=" + seqno, "");
                        break;
                    case "RASPATUR":
                        openWinRaspro("<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RaspaturEntry.aspx?mod=" + mod +"&rsno=<%=hdnRasproSeqNo.Value%>&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&seqno=" + seqno, "");
                        break;
                    default:
                        break;
                }
            }

            function openWinRaspro(url, title) {
                var oWnd = $find("<%= winRaspro.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title(title);
                oWnd.setActive();
                oWnd.show();
                oWnd.maximize();

                //// Cek position
                //var pos = oWnd.getWindowBounds();
                //if (pos.y < 0)
                //    oWnd.moveTo(pos.x, 0);
            }

            function winRaspro_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    // Postback untuk merefresh Info Raspro 
                    var urlEdit = '<%= Helper.UrlRoot2() %>/Module/RADT/Cpoe/EmrCommon/Udd/UddItemEntry.aspx?mod=view&patid=<%= PatientID %>&regno=<%= RegistrationNo %>';
                    document.location.href = urlEdit;
                }
            }

            function openRasproFormView(patid, regno, rseqno) {
                openWindowMaxScreen("<%= Helper.UrlRoot() %>/Module/RADT/Ppra/RasproFormView.aspx?patid=" + patid + "&regno=" + regno + "&rseqno=" + rseqno)
            }
            function openWindowMaxScreen(url) {
                if (url.includes("&rt=") || url.includes("?rt="))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Maximize, Close,Move" Modal="True" ShowOnTopWhenMaximized="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRaspro" runat="server" Modal="True" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Behaviors="None" OnClientClose="winRaspro_ClientClose">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnLocationID" />
    <asp:HiddenField runat="server" ID="hdnFornas" />
    <asp:HiddenField runat="server" ID="hdnFromRegistrationNo" />
    <asp:HiddenField runat="server" ID="hdnRegistrationType" />

    <asp:HiddenField runat="server" ID="hdnRasproSeqNo" />
    <asp:HiddenField runat="server" ID="hdnRasproRefNo" />
    <asp:HiddenField runat="server" ID="hdnRasproType" />
    <asp:HiddenField runat="server" ID="hdnAbLevel" />
    <asp:HiddenField runat="server" ID="hdnAbRestrictionID" />
    <asp:HiddenField runat="server" ID="hdnRasproSeqNo4Filter" />
    <asp:HiddenField runat="server" ID="hdnRasproIsNew" />

    <table cellpadding="0" width="100%">
        <tr>
            <td style="width: 854px;" valign="top">
                <asp:Label runat="server" ID="lblZatActiveInteraction"></asp:Label>
                <fieldset>
                    <legend>UDD Item</legend>
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Dispensary"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Dispensary required."
                                    ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdUddItem" Width="98%" runat="server" OnNeedDataSource="grdUddItem_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdUddItem_UpdateCommand"
                        OnDeleteCommand="grdUddItem_DeleteCommand" OnInsertCommand="grdUddItem_InsertCommand"
                        OnItemCommand="grdUddItem_ItemCommand" OnItemDataBound="grdUddItem_ItemDataBound"
                        ShowHeader="true" AllowMultiRowEdit="false">
                        <MasterTableView DataKeyNames="SequenceNo" CommandItemDisplay="Top">
                            <CommandItemTemplate>
                                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdUddItem.MasterTableView.IsItemInserted %>'>
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                </asp:LinkButton>
                            </CommandItemTemplate>
                            <CommandItemStyle Height="29px" />
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditButton" Visible="true">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="" UniqueName="SequenceNo"
                                    HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="" UniqueName="ParentNo"
                                    HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsRFlag" HeaderText="R/"
                                    UniqueName="IsRFlag" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    Visible="False" />
                                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" Visible="false" />
                                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>
                                        <%# (DataBinder.Eval(Container.DataItem, "RasproSeqNo").ToInt()==0) || false.Equals(DataBinder.Eval(Container.DataItem, "IsAntibiotic"))? String.Empty : string.Format("&nbsp;&nbsp;<a href=\"#\" onclick=\"javascript:openRasproFormView('{0}','{1}','{2}'); return false;\"><img src=\"../../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Raspro Form\" title=\"{3}\" /></a>",
                                            PatientID,RegistrationNo,DataBinder.Eval(Container.DataItem, "RasproSeqNo"),DataBinder.Eval(Container.DataItem, "SRRaspro"))%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Qty" UniqueName="TemplateResultQty" HeaderStyle-Width="50px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResultQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemQtyInString") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateSRItemUnit" HeaderStyle-Width="70px"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? 
                                string.Format("{0} @{1} {2}",DataBinder.Eval(Container.DataItem, "EmbalaceLabel"),DataBinder.Eval(Container.DataItem, "DosageQty"),DataBinder.Eval(Container.DataItem, "SRDosageUnit")) : DataBinder.Eval(Container.DataItem, "SRItemUnit") %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Consume Met." UniqueName="SRConsumeMethodName" HeaderStyle-Width="120px"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) && DataBinder.Eval(Container.DataItem, "ParentNo") != null && !string.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "ParentNo").ToString())
                        ? string.Empty : string.Format("{0} @{1} {2}", DataBinder.Eval(Container.DataItem, "SRConsumeMethodName"),  DataBinder.Eval(Container.DataItem, "ConsumeQty"),   DataBinder.Eval(Container.DataItem, "SRConsumeUnit")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn DataField="StartDateTime" HeaderText="Start" UniqueName="StartDateTime" HeaderStyle-Width="90px"></telerik:GridDateTimeColumn>
                                <telerik:GridTemplateColumn HeaderText="Day" UniqueName="Day"
                                    HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Eval("ConsumeDayNo")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                    HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="LastUpdateByUserName" HeaderText="Last Upd By" UniqueName="LastUpdateByUserName"
                                    HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <%--Begin untuk data pada event ItemDataBound--%>
                                <telerik:GridBoundColumn DataField="IsStop" HeaderText="IsStop" UniqueName="IsStop" Visible="false" />
                                <telerik:GridBoundColumn DataField="IsAntibiotic" HeaderText="IsAntibiotic" UniqueName="IsAntibiotic" Visible="false" />
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="ItemID" UniqueName="ItemID" Visible="false" />
                                <telerik:GridBoundColumn DataField="SRConsumeMethod" HeaderText="SRConsumeMethod" UniqueName="SRConsumeMethod" Visible="false" />
                                <telerik:GridBoundColumn DataField="ConsumeQty" HeaderText="ConsumeQty" UniqueName="ConsumeQty" Visible="false" />
                                <telerik:GridBoundColumn DataField="SRConsumeUnit" HeaderText="SRConsumeUnit" UniqueName="SRConsumeUnit" Visible="false" />
                                <telerik:GridBoundColumn DataField="IsOldRecord" HeaderText="IsOldRecord" UniqueName="IsOldRecord" Visible="false" />
                                <telerik:GridBoundColumn DataField="RasproSeqNo" HeaderText="RasproSeqNo" UniqueName="RasproSeqNo" Visible="false" />
                                <telerik:GridBoundColumn DataField="ConsumeDayNo" HeaderText="ConsumeDayNo" UniqueName="ConsumeDayNo" Visible="false" />
                                <%--End untuk data pada event ItemDataBound--%>
                                <telerik:GridTemplateColumn UniqueName="StopContinueButton" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkStop" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "SequenceNo")%>'
                                            CommandName="stop" ToolTip="Stop UDD" OnClientClick="return confirm('Stop this drug?')">
                                    <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../../Images/Toolbar/blacklist.png" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkContinue" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "SequenceNo")%>'
                                            CommandName="continue" ToolTip="Continue UDD" OnClientClick="return confirm('Continue this drug?')">
                                    <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../../Images/Toolbar/arrowright_blue16.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteButton" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this Drug?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings UserControlName="UddItemDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="UddItemEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Resizing AllowColumnResize="false" />
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
            </td>
            <td valign="top">
                <asp:Panel runat="server" ID="pnlRaspro">
                    <fieldset>
                        <legend>PPRA Form (Antibiotic Guidance and Form)</legend>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadLinkButton runat="server" ID="lnkNewRasal" Text="RASAL" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Raspro Alur Antibiotik Awal (diisi jika belum ada hasil Kultur)" OnClientClicked="newRasal"></telerik:RadLinkButton>
                                </td>
                                <td>
                                    <telerik:RadLinkButton runat="server" ID="lnkNewRaslan" Text="RASLAN" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Raspro Alur Antibiotik Lanjutan" OnClientClicked="newRaslan"></telerik:RadLinkButton>
                                </td>
                                <td>
                                    <telerik:RadLinkButton runat="server" ID="lnkNewRaspraja" Text="RASPRAJA" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Raspro Antibiotik Berkepanjangan" OnClientClicked="newRaspraja"></telerik:RadLinkButton>
                                </td>
                                <td>
                                    <telerik:RadLinkButton runat="server" ID="lnkNewRaspatur" Text="RASPATUR" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Antibiotik Sesuai Kultur (diisi jika sudah ada hasil Kultur)" OnClientClicked="newRaspatur"></telerik:RadLinkButton>
                                </td>
                                <td></td>
                            </tr>
                        </table>

                    </fieldset>
                </asp:Panel>

                <cc:CollapsePanel ID="cpnAllergies" runat="server" Width="100%" Title="Allergies">
                    <asp:Literal runat="server" ID="litPatientAllergy"></asp:Literal>

                    <div style="height: 10px;">
                    </div>
                </cc:CollapsePanel>
                <cc:CollapsePanel ID="CollapsePanel1" runat="server" Width="100%" Title="Vital Sign">
                    <telerik:RadGrid ID="grdVitalSign" runat="server" OnNeedDataSource="grdVitalSign_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView DataKeyNames="VitalSignID" ShowHeader="False">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Question">
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                            <%#DataBinder.Eval(Container.DataItem, "VitalSignName")%>
                                        </a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Answer" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <span>
                                            <%# string.Format("<div style='background-color: {0};width:100%;padding-left: 2px'>{1}</div>",DataBinder.Eval(Container.DataItem, "EwsLevelColor"),DataBinder.Eval(Container.DataItem, "QuestionAnswerFormatted"))%>
                                        </span>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn DataField="RecordDate" UniqueName="RecordDate" HeaderText="Date" HeaderStyle-Width="72px"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="RecordTime" UniqueName="RecordTime" HeaderText="Time" HeaderStyle-Width="43px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="" Visible="false">
                                    <HeaderStyle Width="30px"></HeaderStyle>
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                            <img src='../../../../../Images/Toolbar/barchart.bmp' alt='chart' /></a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText=""></telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Selecting AllowRowSelect="false" />
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <div style="height: 10px">
                    </div>
                </cc:CollapsePanel>
                <cc:CollapsePanel ID="clpAntibioticSuggest" runat="server" Width="100%" Title="Antibiotic Suggestion">
                    <asp:Literal runat="server" ID="litRasproInfo"></asp:Literal>
                    <asp:Literal runat="server" ID="litAntibioticSuggest"></asp:Literal>
                    <telerik:RadGrid ID="grdLaboratoryCultureResult" runat="server" AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView DataKeyNames="OrderLabNo" GroupLoadMode="Client">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="TestGroup" HeaderText="Group" />
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="TestGroup" SortOrder="None" />
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="LabOrderSummary" UniqueName="LabOrderSummary"
                                    HeaderText="Exam Name" HeaderStyle-Width="250px">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "LabOrderSummary")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn DataField="ResultDatetime" UniqueName="ResultDatetime" HeaderText="Result Date" HeaderStyle-Width="120px" />
                                <telerik:GridBoundColumn DataField="Flag" UniqueName="Flag" HeaderText="Flag" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Result" HeaderStyle-Width="150px">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Result")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="StandarValue" UniqueName="StandarValue" HeaderText="Standard Value"
                                    HeaderStyle-Width="150px" />
                                <telerik:GridBoundColumn DataField="ResultComment" UniqueName="ResultComment" HeaderText="Result Comment" />
                                <telerik:GridBoundColumn DataField="LabOrderCode" UniqueName="LabOrderCode" HeaderText="Code" Display="False" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="False">
                            <Selecting AllowRowSelect="False" />
                            <Scrolling UseStaticHeaders="True" />
                        </ClientSettings>
                    </telerik:RadGrid>

                    <br />
                    <div style="height: 10px">
                    </div>
                </cc:CollapsePanel>
            </td>
        </tr>
    </table>
</asp:Content>
