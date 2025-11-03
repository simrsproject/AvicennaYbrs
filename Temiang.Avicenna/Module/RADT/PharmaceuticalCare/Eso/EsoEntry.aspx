<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="EsoEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.EsoEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="codeBlock">
        <script type="text/javascript" language="javascript">
            var _height = 0;
            var _width = 0;
            function onWinPickerClose(sender, args) {

                // Restore window size
                var curWnd = GetRadWindow();
                if (_width > 0) {
                    curWnd.setSize(_width, _height);
                    curWnd.center();
                }

                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    switch (arg.callbackMethod) {
                        case "submit":
                            {
                                __doPostBack(arg.eventTarget, arg.eventArgument);
                                break;
                            }
                        case "rebind":
                            {
                                var ctl = $find(arg.eventTarget);
                                if (typeof ctl.rebind == 'function') {
                                    ctl.rebind();
                                } else {
                                    var masterTable = $find(arg.eventTarget).get_masterTableView();
                                    masterTable.rebind();
                                }
                                break;
                            }
                        case "setvalue":
                            {
                                var ctl = $find(arg.eventTarget);
                                ctl.set_value(arg.value);
                                break;
                            }
                    }
                }
            }


            function openDrugItemPicker() {
                openWinDialog("EsoDrugItemPicker.aspx?regno=<%=RegistrationNo%>&esono=<%=txtEsoNo.Text%>&ccm=rebind&cet=<%=grdItem.ClientID %>");
            }

            function openLaboratoryHist() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/SelectLabHistory.aspx?regno=<%= RegistrationNo %>&ccm=setvalue&cea=lab&textmode=1&cet=<%=txtLaboratoryTest.ClientID %>';
                openWinDialog(url);
            }

            function openWinDialog(url) {
                // Resize window
                var curWnd = GetRadWindow();
                var curWndBounds = curWnd.getWindowBounds();
                // Not Maximized
                if (curWndBounds.y > 0 & curWndBounds.x > 0) {
                    _height = curWndBounds.height;
                    _width = curWndBounds.width;

                    var setHeight = 700;
                    if (setHeight < _height)
                        setHeight = _height;

                    var setWidth = 1200;
                    if (setWidth < _width)
                        setWidth = _width;

                    curWnd.setSize(setWidth, setHeight);
                    curWnd.center();
                }

                var oWnd = $find("<%= winPicker.ClientID %>");
                oWnd.setUrl(url);

                oWnd.setSize(1000, 600);
                oWnd.center();
                oWnd.show();
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winPicker" Width="680px" Height="620px" runat="server" Modal="true"
        ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
        OnClientClose="onWinPickerClose">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%" valign="top">
                <fieldset>
                    <legend style="font-weight: bold; font-size: large; color: yellow; background-color: black; text-align: center;">
                        <asp:Label runat="server" ID="lblPatientName" />
                    </legend>
                    
                    <table width="100%">
                        <tr>
                            <td style="vertical-align: top; width: 125px">
                                <fieldset style="min-height: 125px;">
                                    <div>
                                        <asp:Image runat="server" ID="imgPatientPhoto" Width="120px" Height="120px" />
                                    </div>
                                </fieldset>
                            </td>
                            <td>
                                <table style="width: 100%" valign="top">
                                    <tr>
                                        <td width="100px">MRN
                                        </td>
                                        <td width="4px">:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblMedicalNo" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Reg. No
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblRegistrationNo" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Reg. Date
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblRegistrationDate" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Gender
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblGender" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">DOB / Age
                                        </td>
                                        <td style="vertical-align: top;">:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDateOfBirth" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">Ethnic
                                        </td>
                                        <td style="vertical-align: top;">:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblEthnic" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">Body Weight
                                        </td>
                                        <td style="vertical-align: top;">:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBodyWeight" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">Occupation
                                        </td>
                                        <td style="vertical-align: top;">:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblOccupation" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">Service Unit
                                        </td>
                                        <td style="vertical-align: top;">:
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblServiceUnit" Font-Bold="true" />
                                        </td>
                                    </tr>

                                </table>

                            </td>
                        </tr>

                    </table>


                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td class="label">No 
                            </td>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtEsoNo" runat="server" Width="50px" Enabled="false" />
                                        </td>
                                        <td style="width: 6px;">&nbsp;</td>
                                        <td class="label">Date 
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDateTimePicker ID="txtEsoDateTime" runat="server" Width="170px" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Date required."
                                                ValidationGroup="entry" ControlToValidate="txtEsoDateTime" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Main Disease 
                            </td>
                            <td >
                                <telerik:RadTextBox ID="txtMainDisease" runat="server" TextMode="MultiLine" Width="400px" Resize="Vertical" Height="50px" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Main Disease required."
                                    ValidationGroup="entry" ControlToValidate="txtMainDisease" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Pregnant Status
                            </td>
                            <td >
                                <telerik:RadRadioButtonList ID="optPregnantStatus" runat="server" Width="100%" Columns="3" AutoPostBack="false">
                                    <Items>
                                        <telerik:ButtonListItem Text="Pregnant" Value="1" />
                                        <telerik:ButtonListItem Text="No Pregnant" Value="0" />
                                        <telerik:ButtonListItem Text="Unknow" Value="U" />
                                    </Items>
                                </telerik:RadRadioButtonList>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Other accompanying diseases / conditions
                            </td>
                            <td >
                                <telerik:RadCheckBoxList ID="cblEsoComorbidities" runat="server" Width="100%" Columns="3" AutoPostBack="false">
                                </telerik:RadCheckBoxList>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>

                    </table>
                </fieldset>

            </td>
            <td style="width: 50%" valign="top">

                <fieldset>
                    <legend>
                        <h3>.: Drug Side Effects</h3>
                    </legend>

                    <table width="100%">
                        <tr>
                            <td class="label">Forms / manifestations that occur
                            </td>
                            <td class="entry">
                                <telerik:RadCheckBoxList ID="cblEsoManifestations" runat="server" Width="100%" Columns="3" AutoPostBack="false">
                                </telerik:RadCheckBoxList>
                                <table width="100%">
                                    <tr>
                                        <td class="label">Other Notes
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEsoOtherManifestation" runat="server" TextMode="MultiLine" Width="100%" />
                                        </td>
                                        <td></td>
                                    </tr>

                                </table>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Start Date
                            </td>
                            <td class="entry">
                                <telerik:RadDateTimePicker ID="txtStartDateTime" runat="server" Width="170px" />

                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">End Date
                            </td>
                            <td class="entry">
                                <telerik:RadDateTimePicker ID="txtEndDateTime" runat="server" Width="170px" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">End Status
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSREsoStatus" runat="server" Width="100%">
                                </telerik:RadComboBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Past history of drug side effects 
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPrevEsoHistory" runat="server" TextMode="MultiLine" Width="100%" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>

    </table>

    <fieldset>
        <legend>
            <h3>.: Drug List</h3>
        </legend>

        <telerik:RadGrid ID="grdItem" Width="99%" runat="server" RenderMode="Lightweight"
            AutoGenerateColumns="False" EnableViewState="true"
            AllowMultiRowSelection="True"
            OnNeedDataSource="grdItem_NeedDataSource" OnDeleteCommand="grdItem_DeleteCommand">
            <MasterTableView DataKeyNames="MedicationReceiveNo" ShowHeader="true" ShowHeadersWhenNoRecords="true" Width="100%" CommandItemDisplay="Top">
                <CommandItemTemplate>
                    <asp:LinkButton ID="lbOpenPicker" runat="server" Visible='<%# (DataModeCurrent != AppEnum.DataMode.Read) %>' OnClientClick="openDrugItemPicker();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                    &nbsp;Add Drug Item
                    </asp:LinkButton>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <ColumnGroups>
                    <telerik:GridColumnGroup Name="Consumption" HeaderText="Consumption" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
                </ColumnGroups>
                <Columns>
                    <telerik:GridBoundColumn DataField="ItemDescription" HeaderText="Item Description" UniqueName="ItemDescription" HeaderStyle-Width="250px"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="Suspect" UniqueName="Select" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <center>
                                <label class="switch">
                                    <%# string.Format("<input id=\"chkOnOff\" type=\"checkbox\" name=\"chkOnOff_{2}\" {0} {1}/>",DataBinder.Eval(Container.DataItem, "IsSuspect").Equals(true)?"checked=\"checked\"":string.Empty, DataModeCurrent == AppEnum.DataMode.Read ?"disabled=\"disabled\"":string.Empty, DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"))%>
                                    <span class="slider round"></span>
                                </label>
                            </center>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="ConsumeMethod" HeaderText="Consume Method" UniqueName="ConsumeMethod" HeaderStyle-Width="200px" ColumnGroupName="Consumption">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="StartConsumeDateTime" HeaderText="Start" UniqueName="StartConsumeDateTime" HeaderStyle-Width="100px" ColumnGroupName="Consumption">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="EndConsumeDateTime" HeaderText="End" UniqueName="EndConsumeDateTime" HeaderStyle-Width="100px" ColumnGroupName="Consumption">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridTemplateColumn HeaderText="Consume Indication" UniqueName="Notes" HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadTextBox
                                ID="txtConsumeIndication" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "ConsumeIndication")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                                Width="100%">
                            </telerik:RadTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete" Visible='<%# (DataModeCurrent != AppEnum.DataMode.Read) %>'
                                OnClientClick="javascript: if (!confirm('Delete this drug item ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="<%#Helper.UrlRoot()%>/Images/Toolbar/row_delete16.png" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                <Resizing AllowColumnResize="False" />
                <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
            </ClientSettings>
        </telerik:RadGrid>
    </fieldset>

    <br />

    <table width="100%">
        <tr>
            <td class="label" style="width: 50%;">
                <h3>.: Drug Side Effect Probability Scale "Naranjo" Method</h3>
            </td>
            <td class="label">
                <h3>.: Laboratory Test Result&nbsp;
                    <asp:LinkButton runat="server" ID="lbtnLookUpLab" OnClientClick="openLaboratoryHist();return false;">
                    <img src="../../../../Images/Toolbar/details16.png"/>
                    </asp:LinkButton></h3>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <telerik:RadGrid ID="grdScale" Width="100%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                    AllowMultiRowSelection="True"
                    OnNeedDataSource="grdScale_NeedDataSource" OnItemDataBound="grdScale_ItemDataBound" OnItemCommand="grdScale_ItemCommand" OnPreRender="grdScale_PreRender">
                    <MasterTableView DataKeyNames="ItemID" ShowHeader="true" ShowHeadersWhenNoRecords="true" Width="100%" ShowGroupFooter="true" ShowFooter="false">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="GroupField"></telerik:GridGroupByField>
                                </GroupByFields>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="GroupField" HeaderText=" "></telerik:GridGroupByField>
                                </SelectFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ItemID" HeaderText="No" UniqueName="ItemID" HeaderStyle-Width="40px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText=" " UniqueName="ItemName" HeaderStyle-Width="380px" Aggregate="Custom" FooterText="">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ScaleStatus" UniqueName="ScaleStatus" HeaderText="ScaleStatus" Display="false" />
                            <telerik:GridTemplateColumn HeaderText="" UniqueName="optScaleStatus" HeaderStyle-Width="220px">
                                <ItemTemplate>
                                    <telerik:RadRadioButtonList runat="server" ID="optScaleStatus" AutoPostBack="true" Direction="Horizontal">
                                        <Items>
                                            <telerik:ButtonListItem Text="Ya" Value="1" />
                                            <telerik:ButtonListItem Text="Tidak" Value="2" />
                                            <telerik:ButtonListItem Text="Tidak Tahu" Value="3" />
                                        </Items>
                                    </telerik:RadRadioButtonList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridNumericColumn DataField="ScaleValue" HeaderText="Score" UniqueName="ScaleValue"
                                HeaderStyle-Width="60px" Aggregate="Sum" FooterText="Total: "
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                        <Resizing AllowColumnResize="False" />
                        <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                    </ClientSettings>
                </telerik:RadGrid>
                <fieldset>
                    <legend>Naranjo Score Category</legend>
                    <table>
                        <tr>
                            <td>
                                <ul>
                                    <li>(>=9) Exact</li>
                                    <li>(5-8) Most Probably</li>
                                </ul>
                            </td>
                            <td>
                                <ul>
                                    <li>(1-4) Little Possibility</li>
                                    <li>(0) Doubtful</li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td valign="top">
                <telerik:RadTextBox ID="txtLaboratoryTest" runat="server" TextMode="MultiLine" Width="100%" Height="300px" Resize="Vertical" />
                <br />
                <h3>.: Pharmacist Assesment / Notes</h3>
                <telerik:RadTextBox ID="txtAssessmentNote" runat="server" TextMode="MultiLine" Width="100%" Height="144px" Resize="Vertical" />
            </td>
        </tr>
    </table>
</asp:Content>
