<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OdontogramCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.OdontogramCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>


<telerik:RadCodeBlock runat="server">
    <style>
    .teethRear
    {
        width: 39px;
        height: 35px;
        background-image: url("<%=Helper.UrlRoot()%>/Images/Asset/Odontogram/TeethRear.jpg");
        text-align: center;
        font-size: large;
        font-weight: bold;
        color: red;
    }

    .teethFront
    {
        width: 39px;
        height: 35px;
        background-image: url("<%=Helper.UrlRoot()%>/Images/Asset/Odontogram/TeethFront.jpg");
        text-align: center;
        font-size: large;
        font-weight: bold;
        color: red;
    }

    .RadContextMenu1
    {
        z-index: 999999 !important;
    }
</style>
    <script type="text/javascript">

        function mnuTeeth_OnClientItemClicked(sender, args) {
            var item = args.get_item(),
                itemValue = item.get_value(),
                itemText = item.get_text(),
                itemAttribute = item.get_attributes().getAttribute("Device");

            args.get_targetElement().innerHTML = itemValue;

            var baseClientId = '<%=txt11.ClientID %>'; // ambil id sembarang teeth ctl
            var clientId = baseClientId.substring(0, baseClientId.length - 3) + args.get_targetElement().id;
            var txtCtl = $find(clientId);

            if (txtCtl != null && txtCtl !== 'undefined')
                txtCtl.set_value(itemValue);

        }

    </script>

</telerik:RadCodeBlock>
<telerik:RadContextMenu ID="mnuTeeth" runat="server" OnClientItemClicked="mnuTeeth_OnClientItemClicked"
    EnableRoundedCorners="true" EnableShadows="true">
</telerik:RadContextMenu>

<fieldset style="width: 98%;">
    <legend>ODONTOGRAM</legend>
    <table width="100%">
        <tr>
            <td style="width: 50%;">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="labelcaption">11 [51]</td>
                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt11" runat="server" Width="45%" ReadOnly="true" />
                            <telerik:RadTextBox ID="txt51" runat="server" Width="45%" ReadOnly="true" /></td>
                        <td>
                            <telerik:RadTextBox ID="txt1151Notes" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td class="labelcaption">12 [52]</td>
                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt12" runat="server" Width="45%" ReadOnly="False"/>
                            <telerik:RadTextBox ID="txt52" runat="server" Width="45%" ReadOnly="False"/>

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt1252Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">13 [53]</td>
                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt13" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt53" runat="server" Width="45%" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt1353Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">14 [54]</td>
                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt14" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt54" runat="server" Width="45%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt1454Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">15 [55]</td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt15" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt55" runat="server" Width="45%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt1555Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">16</td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt16" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt16Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">17</td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt17" runat="server" Width="100%" /></td>
                        <td>
                            <telerik:RadTextBox ID="txt17Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">18</td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt18" runat="server" Width="100%" /></td>
                        <td>
                            <telerik:RadTextBox ID="txt18Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%;">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt6121Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt61" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt21" runat="server" Width="45%" />

                        </td>
                        <td class="labelcaption">[61] 21</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt6222Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt62" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt22" runat="server" Width="45%" />

                        </td>
                        <td class="labelcaption">[62] 22</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt6323Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt63" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt23" runat="server" Width="45%" />
                        </td>
                        <td class="labelcaption">[63] 23</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt6424Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt64" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt24" runat="server" Width="45%" />

                        </td>
                        <td class="labelcaption">[64] 24</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt6525Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt65" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt25" runat="server" Width="45%" />

                        </td>
                        <td class="labelcaption">[65] 25</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt26Notes" runat="server" Width="100%" />
                        </td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt26" runat="server" Width="100%" /></td>

                        <td class="labelcaption">26</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt27Notes" runat="server" Width="100%" />
                        </td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt27" runat="server" Width="100%" /></td>

                        <td class="labelcaption">27</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt28Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt28" runat="server" Width="100%" /></td>
                        <td class="labelcaption">28</td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>

    <center>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="labelcaption">18</td>
        <td class="labelcaption">17</td>
        <td class="labelcaption">16</td>
        <td class="labelcaption">15</td>
        <td class="labelcaption">14</td>
        <td class="labelcaption">13</td>
        <td class="labelcaption">12</td>
        <td class="labelcaption">11</td>
        <td class="labelcaption">21</td>
        <td class="labelcaption">22</td>
        <td class="labelcaption">23</td>
        <td class="labelcaption">24</td>
        <td class="labelcaption">25</td>
        <td class="labelcaption">26</td>
        <td class="labelcaption">27</td>
        <td class="labelcaption">28</td>
    </tr>
    <tr>
        <td id="t18" class="teethRear"><%=Odontogram.T18 %></td>
        <td id="t17" class="teethRear"><%=Odontogram.T17 %></td>
        <td id="t16" class="teethRear"><%=Odontogram.T16 %></td>
        <td id="t15" class="teethRear"><%=Odontogram.T15 %></td>
        <td id="t14" class="teethRear"><%=Odontogram.T14 %></td>
        <td id="t13" class="teethFront"><%=Odontogram.T13 %></td>
        <td id="t12" class="teethFront"><%=Odontogram.T12 %></td>
        <td id="t11" class="teethFront"><%=Odontogram.T11 %></td>
        <td id="t21" class="teethFront"><%=Odontogram.T21 %></td>
        <td id="t22" class="teethFront"><%=Odontogram.T22 %></td>
        <td id="t23" class="teethFront"><%=Odontogram.T23 %></td>
        <td id="t24" class="teethRear"><%=Odontogram.T24 %></td>
        <td id="t25" class="teethRear"><%=Odontogram.T25 %></td>
        <td id="t26" class="teethRear"><%=Odontogram.T26 %></td>
        <td id="t27" class="teethRear"><%=Odontogram.T27 %></td>
        <td id="t28" class="teethRear"><%=Odontogram.T28 %></td>
    </tr>
    <tr>
        <td>
            <br />
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td class="labelcaption">55</td>
        <td class="labelcaption">54</td>
        <td class="labelcaption">53</td>
        <td class="labelcaption">52</td>
        <td class="labelcaption">51</td>
        <td class="labelcaption">61</td>
        <td class="labelcaption">62</td>
        <td class="labelcaption">63</td>
        <td class="labelcaption">64</td>
        <td class="labelcaption">65</td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td id="t55" class="teethRear"><%=Odontogram.T55 %></td>
        <td id="t54" class="teethRear"><%=Odontogram.T54 %></td>
        <td id="t53" class="teethFront"><%=Odontogram.T53 %></td>
        <td id="t52" class="teethFront"><%=Odontogram.T52 %></td>
        <td id="t51" class="teethFront"><%=Odontogram.T51 %></td>
        <td id="t61" class="teethFront"><%=Odontogram.T61 %></td>
        <td id="t62" class="teethFront"><%=Odontogram.T62 %></td>
        <td id="t63" class="teethFront"><%=Odontogram.T63 %></td>
        <td id="t64" class="teethRear"><%=Odontogram.T64 %></td>
        <td id="t65" class="teethRear"><%=Odontogram.T65 %></td>
        <td></td>
        <td></td>
        <td></td>

    </tr>
    <tr>
        <td>
            <br />
        </td>
    </tr>

    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td id="t85" class="teethRear"><%=Odontogram.T85 %></td>
        <td id="t84" class="teethRear"><%=Odontogram.T84 %></td>
        <td id="t83" class="teethFront"><%=Odontogram.T83 %></td>
        <td id="t82" class="teethFront"><%=Odontogram.T82 %></td>
        <td id="t81" class="teethFront"><%=Odontogram.T81 %></td>
        <td id="t71" class="teethFront"><%=Odontogram.T71 %></td>
        <td id="t72" class="teethFront"><%=Odontogram.T72 %></td>
        <td id="t73" class="teethFront"><%=Odontogram.T73 %></td>
        <td id="t74" class="teethRear"><%=Odontogram.T74 %></td>
        <td id="t75" class="teethRear"><%=Odontogram.T75 %></td>
        <td></td>
        <td></td>
        <td></td>

    </tr>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td class="labelcaption">85</td>
        <td class="labelcaption">84</td>
        <td class="labelcaption">83</td>
        <td class="labelcaption">82</td>
        <td class="labelcaption">81</td>
        <td class="labelcaption">71</td>
        <td class="labelcaption">72</td>
        <td class="labelcaption">73</td>
        <td class="labelcaption">74</td>
        <td class="labelcaption">75</td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <br />
        </td>
    </tr>
    <tr>
        <td id="t48" class="teethRear"><%=Odontogram.T48 %></td>
        <td id="t47" class="teethRear"><%=Odontogram.T47 %></td>
        <td id="t46" class="teethRear"><%=Odontogram.T46 %></td>
        <td id="t45" class="teethRear"><%=Odontogram.T45 %></td>
        <td id="t44" class="teethRear"><%=Odontogram.T44 %></td>
        <td id="t43" class="teethFront"><%=Odontogram.T43 %></td>
        <td id="t42" class="teethFront"><%=Odontogram.T42 %></td>
        <td id="t41" class="teethFront"><%=Odontogram.T41 %></td>
        <td id="t31" class="teethFront"><%=Odontogram.T31 %></td>
        <td id="t32" class="teethFront"><%=Odontogram.T32 %></td>
        <td id="t33" class="teethFront"><%=Odontogram.T33 %></td>
        <td id="t34" class="teethRear"><%=Odontogram.T34 %></td>
        <td id="t35" class="teethRear"><%=Odontogram.T35 %></td>
        <td id="t36" class="teethRear"><%=Odontogram.T36 %></td>
        <td id="t37" class="teethRear"><%=Odontogram.T37 %></td>
        <td id="t38" class="teethRear"><%=Odontogram.T38 %></td>
    </tr>
    <tr>
        <td class="labelcaption">48</td>
        <td class="labelcaption">47</td>
        <td class="labelcaption">46</td>
        <td class="labelcaption">45</td>
        <td class="labelcaption">44</td>
        <td class="labelcaption">43</td>
        <td class="labelcaption">42</td>
        <td class="labelcaption">41</td>
        <td class="labelcaption">31</td>
        <td class="labelcaption">32</td>
        <td class="labelcaption">33</td>
        <td class="labelcaption">34</td>
        <td class="labelcaption">35</td>
        <td class="labelcaption">36</td>
        <td class="labelcaption">37</td>
        <td class="labelcaption">38</td>
    </tr>
</table>
</center>
    <table width="100%">
        <tr>
            <td style="width: 50%;">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="labelcaption">48</td>
                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt48" runat="server" Width="100%" /></td>
                        <td>
                            <telerik:RadTextBox ID="txt48Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">47</td>
                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt47" runat="server" Width="100%" /></td>
                        <td>
                            <telerik:RadTextBox ID="txt47Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">46</td>
                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt46" runat="server" Width="100%" /></td>
                        <td>
                            <telerik:RadTextBox ID="txt46Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">45 [85]</td>
                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt45" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt85" runat="server" Width="45%" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt4585Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">44 [84]</td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt44" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt84" runat="server" Width="45%" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt4484Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">43 [83]</td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt43" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt83" runat="server" Width="45%" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt4383Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">42 [82]</td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt42" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt82" runat="server" Width="45%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt4282Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcaption">41 [81]</td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt41" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt81" runat="server" Width="45%" />

                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt4181Notes" runat="server" Width="100%" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%;">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt38Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt38" runat="server" Width="100%" /></td>
                        <td class="labelcaption">38</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt37Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt37" runat="server" Width="100%" /></td>
                        <td class="labelcaption">37</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt36Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt36" runat="server" Width="100%" /></td>
                        <td class="labelcaption">36</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt7535Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 80px;">
                            <telerik:RadTextBox ID="txt75" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt35" runat="server" Width="45%" />

                        </td>
                        <td class="labelcaption">[75] 35</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt7434Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt74" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt34" runat="server" Width="45%" />

                        </td>
                        <td class="labelcaption">[74] 34</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt7333Notes" runat="server" Width="100%" />
                        </td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt73" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt33" runat="server" Width="45%" />

                        </td>

                        <td class="labelcaption">[73] 33</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt7232Notes" runat="server" Width="100%" />
                        </td>
                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt72" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt32" runat="server" Width="45%" />

                        </td>

                        <td class="labelcaption">[72] 32</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txt7131Notes" runat="server" Width="100%" />
                        </td>

                        <td style="width: 50px;">
                            <telerik:RadTextBox ID="txt71" runat="server" Width="45%" />
                            <telerik:RadTextBox ID="txt31" runat="server" Width="45%" />

                        </td>
                        <td class="labelcaption">[71] 31</td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
</fieldset>
