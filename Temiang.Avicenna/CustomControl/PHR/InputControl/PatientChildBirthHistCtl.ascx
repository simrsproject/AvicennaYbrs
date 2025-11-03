<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientChildBirthHistCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.PatientChildBirthHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<fieldset>
    <legend><b>HISTORY OF PREGNANCY, CHILD BIRTH, AND POSTPARTUM</b></legend>
    <telerik:RadGrid ID="grdPatientChildBirthHist" Width="100%" runat="server" GridLines="None" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
        OnNeedDataSource="grdPatientChildBirthHist_NeedDataSource" OnItemDataBound="grdPatientChildBirthHist_ItemDataBound">
        <MasterTableView DataKeyNames="SequenceNo">
            <Columns>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" HeaderText="No" HeaderStyle-Width="40px" />
                <telerik:GridBoundColumn DataField="ChildBirth" UniqueName="ChildBirth" HeaderText="Partus Year" HeaderStyle-Width="100px" />
                <telerik:GridTemplateColumn HeaderText="Partus Year" UniqueName="ChildBirthEdit" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtChildBirth" runat="server" Width="100%" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Sex" UniqueName="Sex" HeaderText="Sex" HeaderStyle-Width="50px" />
                <telerik:GridTemplateColumn HeaderText="Sex" UniqueName="SexEdit" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <telerik:RadComboBox runat="server" ID="cboSex" Width="100%">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="" Value="" />
                                <telerik:RadComboBoxItem runat="server" Text="Male" Value="M" />
                                <telerik:RadComboBoxItem runat="server" Text="Female" Value="F" />
                            </Items>
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="SRBirthMethod" UniqueName="SRBirthMethod" HeaderText="Birth Method" Display="False" />
                <telerik:GridBoundColumn DataField="BirthMethod" UniqueName="BirthMethod" HeaderText="Birth Method" HeaderStyle-Width="150px" />
                <telerik:GridTemplateColumn HeaderText="Birth Method" UniqueName="BirthMethodEdit" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <telerik:RadComboBox runat="server" ID="cboSRBirthMethod" Width="100%"></telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Helper" UniqueName="Helper" HeaderText="Helper" HeaderStyle-Width="150px" />
                <telerik:GridTemplateColumn HeaderText="Helper" UniqueName="HelperEdit" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtHelper" runat="server" Width="100%" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridBoundColumn DataField="PregnanDurationWeek" UniqueName="PregnanDurationWeek" HeaderText="PregnanDurationWeek" Display="False"/>
                <%--<telerik:GridBoundColumn DataField="PregnanDurationMonth" UniqueName="PregnanDurationMonth" HeaderText="PregnanDurationMonth" Display="False" />--%>
                <telerik:GridBoundColumn DataField="PregnanDurationDay" UniqueName="PregnanDurationDay" HeaderText="PregnanDurationDay" Display="False" />

                <telerik:GridTemplateColumn HeaderText="Pregnant Duration" UniqueName="PregnanDuration" HeaderStyle-Width="120px">
                    <ItemTemplate>
                        <%#string.Format("{0} W {1} D", Eval("PregnanDurationWeek"),Eval("PregnanDurationDay")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Pregnant Duration" UniqueName="PregnanDurationEdit" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <%--<telerik:RadNumericTextBox ID="txtPregnanDurationMonth" runat="server" Width="30px" NumberFormat-DecimalDigits="0" />&nbsp;M&nbsp;--%>
                        <telerik:RadNumericTextBox ID="txtPregnanDurationWeek" runat="server" Width="30px" NumberFormat-DecimalDigits="0" />&nbsp;W&nbsp;&nbsp;
                        <telerik:RadNumericTextBox ID="txtPregnanDurationDay" runat="server" Width="30px" NumberFormat-DecimalDigits="0" />&nbsp;D
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Location" UniqueName="Location" HeaderText="Partus Location" HeaderStyle-Width="150px" />
                <telerik:GridTemplateColumn HeaderText="Location" UniqueName="LocationEdit" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtLocation" runat="server" Width="100%" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="HM" UniqueName="HM" HeaderText="Baby Condition" HeaderStyle-Width="40px" />
                <telerik:GridTemplateColumn HeaderText="Baby Condition" UniqueName="HMEdit" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <telerik:RadComboBox runat="server" ID="cboHM" Width="100%">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="" Value="" />
                                <telerik:RadComboBoxItem runat="server" Text="Alive" Value="A" />
                                <telerik:RadComboBoxItem runat="server" Text="Disability" Value="B" />
                                <telerik:RadComboBoxItem runat="server" Text="Dead" Value="C" />
                            </Items>
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="BBL" UniqueName="BBL" HeaderText="BW (gr)" HeaderStyle-Width="80px" />
                <telerik:GridTemplateColumn HeaderText="BW (gr)" UniqueName="BBLEdit" HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtBBL" runat="server" Width="50px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Complication" UniqueName="Complication" HeaderText="Complication" HeaderStyle-Width="150px" />
                <telerik:GridTemplateColumn HeaderText="Complication" UniqueName="ComplicationEdit" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtComplication" runat="server" Width="100%" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" HeaderText="Notes" />
                <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="NotesEdit">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
            <Resizing AllowColumnResize="False" />
            <Selecting AllowRowSelect="false"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>
</fieldset>
