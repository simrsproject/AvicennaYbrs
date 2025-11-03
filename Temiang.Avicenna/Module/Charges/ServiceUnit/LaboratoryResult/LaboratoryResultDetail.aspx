<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="LaboratoryResultDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.LaboratoryResultDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function openNormalValueDialog(id) {
                var oWnd = window.$find("<%= winRujukan.ClientID %>");
                oWnd.setUrl("LaboratoryResultItemDialog.aspx?id=" + id);
                oWnd.set_width($(window).width());
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) __doPostBack("<%= grdTransChargesItem.UniqueID %>", oWnd.argument.mode);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="500px" VisibleStatusbar="False" Modal="true" ID="winRujukan"
        OnClientClose="onClientClose" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtTransactionTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="True">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblExecutionDateTime" runat="server" Text="Validated Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtExecutionDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtExecutionTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvExecutionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtExecutionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtServiceUnitID" Width="100px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblServiceUnitName" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" Height="127px" TextMode="MultiLine"/>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="197px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;Y&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;M&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;D
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room / Bed No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRoomName" runat="server" Width="179px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;&nbsp;/&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClass" runat="server" Text="Charge Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClassName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtParamedicName" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox runat="server" ID="txtParamedicID" Visible="False" />
                        </td>
                        <td width="20" />
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtGuarantorName" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Physician In Charge
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="304px" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                    <legend>Photo</legend>
                    <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="MultiPage1" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Offline Result" PageViewID="pgDetail" />
            <telerik:RadTab runat="server" Text="Online Result" PageViewID="pgRiwayat" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="MultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
            <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnItemCommand="grdTransChargesItem_ItemCommand">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo, ItemID"
                    GroupLoadMode="Client">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbInsert" runat="server" CommandName="Rebuild">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/refresh16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Refresh" />
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ItemGroupName" HeaderText="Group Name "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ItemGroupName" SortOrder="None"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="ItemID" UniqueName="ItemID" SortExpression="ItemID"
                            Visible="false" />
                        <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="ItemName">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsResultInput").Equals(false) ? DataBinder.Eval(Container.DataItem, "TestName") :
                            string.Format("<a href=\"#\" onclick=\"openNormalValueDialog('{0}'); return false;\">{1}</a>",
                            DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "TestName")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="NormalValueMin" UniqueName="NormalValueMin" SortExpression="NormalValueMin"
                            HeaderText="Normal Value Min" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="NormalValueMax" UniqueName="NormalValueMax" SortExpression="NormalValueMax"
                            HeaderText="Normal Value Max" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderText="Result Value" UniqueName="ResultValue">
                            <ItemTemplate>
                                <telerik:RadComboBox runat="server" ID="cboResult" Width="100%" Visible="false" />
                                <telerik:RadTextBox runat="server" ID="txtResult" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "ResultValue") %>'
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsResultInput") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ResultValue" UniqueName="ResultValue2" SortExpression="ResultValue2"
                            HeaderText="Result Value" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemUnit" UniqueName="ItemUnit" SortExpression="ItemUnit"
                            HeaderText="Unit" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="Notes">
                            <ItemTemplate>
                                <telerik:RadTextBox runat="server" ID="txtNotes" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Notes") %>'
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsResultInput") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes2" SortExpression="Notes2"
                            HeaderText="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Cito" UniqueName="IsCito"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsCito" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsCito") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Duplo" UniqueName="IsDuplo" HeaderStyle-Width="60px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsDuplo" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsDuplo") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Description" UniqueName="IsDuplo" HeaderStyle-Width="100px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsDescription" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsDescriptionResult") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRiwayat" runat="server">
            <telerik:RadGrid ID="grdOnline" runat="server" OnNeedDataSource="grdOnline_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnItemDataBound="grdOnline_ItemDataBound">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, nourut" GroupLoadMode="Client">
                    <SortExpressions>
                        <telerik:GridSortExpression FieldName="nourut" SortOrder="Ascending" />
                    </SortExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="namapemeriksaan" UniqueName="namapemeriksaan"
                            SortExpression="namapemeriksaan" HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="hasil" UniqueName="hasil" SortExpression="hasil"
                            HeaderText="Result Value" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="StandardValue" UniqueName="StandardValue" SortExpression="StandardValue"
                            HeaderText="Standard Value" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="flag" UniqueName="flag" SortExpression="flag"
                            HeaderText="Status" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ValidateDateTime" HeaderText="Validate Date"
                            UniqueName="ValidateDateTime" SortExpression="ValidateDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ValidateBy" HeaderText="Validated By" UniqueName="ValidateBy"
                            SortExpression="ValidateBy" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="KodePemeriksaan" UniqueName="KodePemeriksaan" SortExpression="KodePemeriksaan"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="OrderTestId" UniqueName="OrderTestId" SortExpression="OrderTestId"
                            Visible="false" />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
