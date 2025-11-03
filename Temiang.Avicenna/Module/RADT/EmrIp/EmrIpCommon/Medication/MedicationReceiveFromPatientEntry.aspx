<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveFromPatientEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationReceiveFromPatientEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphList" runat="server">
   <script type="text/javascript" language="javascript">
        function applyGridHeightMax() {
            var height =
                (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

            // set height to the whole RadGrid control
            var grid = $find("<%= grdMedicationStatus.ClientID %>");
            grid.get_element().style.height = height - 50 + "px";
            grid.repaint();
       }
       window.onload = function () {
           applyGridHeightMax();
       }
       window.onresize = function () {
           applyGridHeightMax();
       }

       // After postback
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       prm.add_endRequest(function (s, e) {
           applyGridHeightMax();
       });
   </script>
    <fieldset>
        <legend>Drug Item Received</legend>
        <telerik:RadGrid ID="grdMedicationStatus" runat="server" OnNeedDataSource="grdMedicationStatus_NeedDataSource" OnDetailTableDataBind="grdMedicationStatus_DetailTableDataBind"
            AutoGenerateColumns="False" GridLines="None" Width="100%"
            OnItemCommand="grdMedicationStatus_ItemCommand" OnItemDataBound="grdMedicationStatus_ItemDataBound">
            <MasterTableView DataKeyNames="MedicationReceiveNo,IsVoid">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnView" runat="server" CommandName="View" ToolTip='View'>
                            <img src="../../../../../Images/Toolbar/views16.png" border="0" alt=""/>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ItemDescription" HeaderStyle-Width="200px" HeaderText="Item">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%><br />
                            <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%>
                            <br />
                            <%# DataBinder.Eval(Container.DataItem, "SRMedicationConsumeName")%><br />
                            &nbsp;
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="Reason" UniqueName="Reason" HeaderText="Consume Reason" HeaderStyle-Width="120px" />
                    <telerik:GridBoundColumn DataField="Duration" UniqueName="Duration" HeaderText="Duration" HeaderStyle-Width="60px" />
                    <telerik:GridDateTimeColumn DataField="ReceiveDateTime" UniqueName="ReceiveDateTime" HeaderText="Receive Time"
                        HeaderStyle-Width="110px" />
                    <telerik:GridNumericColumn DataField="ReceiveQty" UniqueName="ReceiveQty" HeaderText="Rec. Qty" HeaderStyle-Width="60px" />
                    <telerik:GridNumericColumn DataField="BalanceQty" UniqueName="BalanceQty" HeaderText="Bal. Qty" HeaderStyle-Width="60px" />
                    <telerik:GridBoundColumn DataField="SRConsumeUnit" UniqueName="SRConsumeUnit" HeaderText="Unit" HeaderStyle-Width="60px" />
                    <telerik:GridDateTimeColumn DataField="StartDateTime" UniqueName="StartDateTime" HeaderText="Continue"
                        HeaderStyle-Width="110px" />
                    <telerik:GridCheckBoxColumn DataField="IsManagedByPatient" UniqueName="IsManagedByPatient" HeaderText="By Patient" HeaderStyle-Width="70px" />

                    <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                </Columns>
                <DetailTables>
                    <telerik:GridTableView DataKeyNames="MedicationReceiveNo, SequenceNo" Name="grdMedicationStatusUsed" Width="100%"
                        AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                        <Columns>
                            <telerik:GridDateTimeColumn DataField="ScheduleDateTime" HeaderText="Schedule" UniqueName="ScheduleDateTime" HeaderStyle-Width="120px" />
                            <telerik:GridDateTimeColumn DataField="SetupDateTime" HeaderText="Setup" UniqueName="SetupDateTime" HeaderStyle-Width="120px" />
                            <telerik:GridBoundColumn DataField="SetupByUserName" UniqueName="SetupByUserName" HeaderText="Setup By" HeaderStyle-Width="120px" />

                            <telerik:GridDateTimeColumn DataField="VerificationDateTime" HeaderText="Verification" UniqueName="VerificationDateTime" HeaderStyle-Width="120px" />
                            <telerik:GridBoundColumn DataField="VerificationByUserName" UniqueName="VerificationByUserName" HeaderText="Verification By" HeaderStyle-Width="120px" />

                            <telerik:GridDateTimeColumn DataField="RealizedDateTime" HeaderText="Realized" UniqueName="RealizedDateTime" HeaderStyle-Width="120px" />
                            <telerik:GridBoundColumn DataField="RealizedByUserName" UniqueName="RealizedByUserName" HeaderText="Realized By" HeaderStyle-Width="120px" />

                            <telerik:GridNumericColumn DataField="Qty" UniqueName="Qty" HeaderText="Qty" DecimalDigits="2" HeaderStyle-Width="60px" />
                            <telerik:GridCheckBoxColumn DataField="IsNotConsume" UniqueName="IsNotConsume" HeaderText="Not Consume" HeaderStyle-Width="70px" />
                            <telerik:GridBoundColumn DataField="Note" UniqueName="Note" HeaderText="Note" />
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
            </ClientSettings>
        </telerik:RadGrid>
    </fieldset>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEntry" runat="server">
    <telerik:RadTabStrip ID="mainRadTabStrip" runat="server" MultiPageID="mainRadMultiPage" ShowBaseLine="true"
        Align="Left" PerTabScrolling="True"
        SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Drug Information" PageViewID="pgMain"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Other Information" PageViewID="pgOther" />
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="mainRadMultiPage" runat="server" SelectedIndex="0" ScrollBars="Auto"
        CssClass="multiPage">

        <telerik:RadPageView ID="pgMain" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="label">Receive Date</td>
                    <td>
                        <telerik:RadDatePicker ID="txtReceiveDateTime" runat="server" Width="100px" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Item</td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboItemID" runat="server" Width="100%" EmptyMessage="Select a Item"
                            EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true"
                            OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnClientFocus="showDropDown">
                            <WebServiceSettings Method="ItemProductMedics" Path="~/WebService/ComboBoxDataService.asmx" />
                            <ClientItemTemplate>
                        <div>
                            <ul class="details">
                                <li class="bold"><span>#= Text # </span></li>
                                <li class="small"><span>#= Attributes.GenericFlag #</span></li>
                                <li class="smaller"><span>Substance:#= Attributes.ZatActive #  </span></li>
                            </ul>
                        </div>
                            </ClientItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                </tr>
                <tr>
                    <td class="label">Item Description</td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemDescription" runat="server" Width="100%" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Item Description required."
                            ValidationGroup="entry" ControlToValidate="txtItemDescription"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">Receive Qty</td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtReceiveQty" runat="server" Width="90px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Receive Qty required."
                            ValidationGroup="entry" ControlToValidate="txtReceiveQty"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">Consume Method</td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboConsumeMethod" Width="100%" EmptyMessage="Select Unit Consume"
                            EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false" OnClientFocus="showDropDown">
                            <WebServiceSettings Method="ConsumeMethods" Path="~/WebService/ComboBoxDataService.asmx" />
                            <ClientItemTemplate>
                                    <div>
                                        <ul class="details">
                                            <li class="bold"><span>#= Text # </span></li>
                                            <li class="small"><span>Time: #= Attributes.TimeSequence #</span></li>
                                        </ul>
                                    </div>
                            </ClientItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Consume Method required."
                            ValidationGroup="entry" ControlToValidate="cboConsumeMethod"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">Qty Every Consume</td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtConsumeQty" runat="server" Width="90px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Consume Qty required."
                            ValidationGroup="entry" ControlToValidate="txtConsumeQty"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">In Unit</td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRConsumeUnit" Width="100%"
                            EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                            OnClientItemsRequesting="cboConsumeUnit_ClientItemsRequesting" OnClientFocus="showDropDown">
                            <WebServiceSettings Method="ConsumeUnits" Path="~/WebService/ComboBoxDataService.asmx" />
                        </telerik:RadComboBox>
                        <script type="text/javascript">
                            function cboConsumeUnit_ClientItemsRequesting(sender, eventArgs) {
                                var context = eventArgs.get_context();
                                context["ItemID"] = $telerik.findControl(sender.get_parent().get_element(), "cboItemID").get_value();
                            }

                            function showDropDown(sender, eventArgs) {
                                sender.showDropDown();
                                sender.requestItems("[showall]", false);
                            }

                        </script>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Unit required."
                            ValidationGroup="entry" ControlToValidate="cboSRConsumeUnit"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">Last Consumption</td>
                    <td>
                        <telerik:RadDatePicker ID="txtLastConsumeDateTime" runat="server" Width="100px" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Consume Reason</td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReason" runat="server" Width="100%" />
                    </td>
                    <td width="20px"></td>
                </tr>
                <tr>
                    <td class="label">Duration of Drug Use</td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDuration" runat="server" Width="100%" />
                    </td>
                    <td width="20px"></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsManagedByPatient" Text="Managed by Patient" />
                    </td>
                    <td width="20px"></td>
                </tr>

            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgOther" runat="server">
            <fieldset>
                <legend><b>Appropriateness</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td class="label">Item Condition</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCondition" runat="server" Width="100%" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">Temperature</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTemp" runat="server" Width="100%" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">Beyond Use Date</td>
                        <td>
                            <telerik:RadDatePicker ID="txtBeyondUseDate" runat="server" Width="100px" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Expire Date</td>
                        <td>
                            <telerik:RadDatePicker ID="txtExpireDate" runat="server" Width="100px" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Summary Appropriate</td>
                        <td>
                            <telerik:RadRadioButtonList runat="server" ID="optIsAppropriate" AutoPostBack="false">
                                <Items>
                                    <telerik:ButtonListItem Text="Yes" Value="1" />
                                    <telerik:ButtonListItem Text="No" Value="0" />
                                </Items>
                            </telerik:RadRadioButtonList>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend><b>Appropriateness</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td class="label">Medication Consume</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMedicationConsume" runat="server" Width="100%" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">Plan Continue Consume</td>
                        <td>
                            <telerik:RadDateTimePicker ID="txtStartDateTime" runat="server" Width="170px" />
                        </td>
                        <td><asp:RequiredFieldValidator ID="rfvStartDateTime" runat="server" ErrorMessage="Plan Continue Consume required."
                            ValidationGroup="entry" ControlToValidate="txtStartDateTime"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator></td>
                    </tr>
                </table>
            </fieldset>

        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
