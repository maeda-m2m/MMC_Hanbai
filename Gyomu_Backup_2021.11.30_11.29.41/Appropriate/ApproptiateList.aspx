<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproptiateList.aspx.cs" Inherits="Gyomu.Appropriate.ApproptiateList" %>

<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <link href="../../MainStyle.css" type="text/css" rel="Stylesheet" />
    <link href="../sheet/MainStyles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            font-family: Tahoma;
            font-size: 9pt;
        }

        .sl {
            border-bottom: solid 1px silver;
        }

        .g {
            BACKGROUND-COLOR: gray
        }

        .column {
            background-color: #00008B;
            color: #FFFFFF;
            padding: 0.5em;
            font-size: 10px;
        }

        .row {
        }

        .RadComboBox_Default {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadComboBox {
            text-align: left;
            display: inline-block;
            vertical-align: middle;
            white-space: nowrap;
            *display: inline;
            *zoom: 1
        }

        .RadComboBox_Default {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadComboBox {
            text-align: left;
            display: inline-block;
            vertical-align: middle;
            white-space: nowrap;
            *display: inline;
            *zoom: 1
        }

        .RadComboBox_Default {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadComboBox {
            text-align: left;
            display: inline-block;
            vertical-align: middle;
            white-space: nowrap;
            *display: inline;
            *zoom: 1
        }

        .RadComboBox_Default {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadComboBox {
            text-align: left;
            display: inline-block;
            vertical-align: middle;
            white-space: nowrap;
            *display: inline;
            *zoom: 1
        }

        .RadComboBox_Default {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadComboBox {
            text-align: left;
            display: inline-block;
            vertical-align: middle;
            white-space: nowrap;
            *display: inline;
            *zoom: 1
        }

        .RadComboBox_Default {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadComboBox {
            text-align: left;
            display: inline-block;
            vertical-align: middle;
            white-space: nowrap;
            *display: inline;
            *zoom: 1
        }

        .RadComboBox_Default {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadComboBox {
            text-align: left;
            display: inline-block;
            vertical-align: middle;
            white-space: nowrap;
            *display: inline;
            *zoom: 1
        }

        .RadComboBox_Default {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadComboBox {
            text-align: left;
            display: inline-block;
            vertical-align: middle;
            white-space: nowrap;
            *display: inline;
            *zoom: 1
        }

            .RadComboBox .rcbReadOnly .rcbInputCellLeft {
                background-position: 0 -88px
            }

            .RadComboBox .rcbReadOnly .rcbInputCellLeft {
                background-position: 0 -88px
            }

            .RadComboBox .rcbReadOnly .rcbInputCellLeft {
                background-position: 0 -88px
            }

            .RadComboBox .rcbReadOnly .rcbInputCellLeft {
                background-position: 0 -88px
            }

            .RadComboBox .rcbReadOnly .rcbInputCellLeft {
                background-position: 0 -88px
            }

            .RadComboBox .rcbReadOnly .rcbInputCellLeft {
                background-position: 0 -88px
            }

            .RadComboBox .rcbReadOnly .rcbInputCellLeft {
                background-position: 0 -88px
            }

            .RadComboBox .rcbReadOnly .rcbInputCellLeft {
                background-position: 0 -88px
            }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbInputCellLeft {
            background-position: 0 0
        }

        .RadComboBox .rcbInputCell {
            padding-right: 4px;
            padding-left: 5px;
            width: 100%;
            height: 20px;
            line-height: 20px;
            text-align: left;
            vertical-align: middle
        }

        .RadComboBox .rcbInputCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbInputCellLeft {
            background-position: 0 0
        }

        .RadComboBox .rcbInputCell {
            padding-right: 4px;
            padding-left: 5px;
            width: 100%;
            height: 20px;
            line-height: 20px;
            text-align: left;
            vertical-align: middle
        }

        .RadComboBox .rcbInputCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbInputCellLeft {
            background-position: 0 0
        }

        .RadComboBox .rcbInputCell {
            padding-right: 4px;
            padding-left: 5px;
            width: 100%;
            height: 20px;
            line-height: 20px;
            text-align: left;
            vertical-align: middle
        }

        .RadComboBox .rcbInputCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbInputCellLeft {
            background-position: 0 0
        }

        .RadComboBox .rcbInputCell {
            padding-right: 4px;
            padding-left: 5px;
            width: 100%;
            height: 20px;
            line-height: 20px;
            text-align: left;
            vertical-align: middle
        }

        .RadComboBox .rcbInputCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbInputCellLeft {
            background-position: 0 0
        }

        .RadComboBox .rcbInputCell {
            padding-right: 4px;
            padding-left: 5px;
            width: 100%;
            height: 20px;
            line-height: 20px;
            text-align: left;
            vertical-align: middle
        }

        .RadComboBox .rcbInputCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbInputCellLeft {
            background-position: 0 0
        }

        .RadComboBox .rcbInputCell {
            padding-right: 4px;
            padding-left: 5px;
            width: 100%;
            height: 20px;
            line-height: 20px;
            text-align: left;
            vertical-align: middle
        }

        .RadComboBox .rcbInputCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbInputCellLeft {
            background-position: 0 0
        }

        .RadComboBox .rcbInputCell {
            padding-right: 4px;
            padding-left: 5px;
            width: 100%;
            height: 20px;
            line-height: 20px;
            text-align: left;
            vertical-align: middle
        }

        .RadComboBox .rcbInputCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbInputCellLeft {
            background-position: 0 0
        }

        .RadComboBox .rcbInputCell {
            padding-right: 4px;
            padding-left: 5px;
            width: 100%;
            height: 20px;
            line-height: 20px;
            text-align: left;
            vertical-align: middle
        }

        .RadComboBox .rcbInputCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 2px 0 1px;
            height: auto;
            width: 100%;
            border-width: 0;
            outline: 0;
            color: inherit;
            background-color: transparent;
            vertical-align: top;
            opacity: 1
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 2px 0 1px;
            height: auto;
            width: 100%;
            border-width: 0;
            outline: 0;
            color: inherit;
            background-color: transparent;
            vertical-align: top;
            opacity: 1
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 2px 0 1px;
            height: auto;
            width: 100%;
            border-width: 0;
            outline: 0;
            color: inherit;
            background-color: transparent;
            vertical-align: top;
            opacity: 1
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 2px 0 1px;
            height: auto;
            width: 100%;
            border-width: 0;
            outline: 0;
            color: inherit;
            background-color: transparent;
            vertical-align: top;
            opacity: 1
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 2px 0 1px;
            height: auto;
            width: 100%;
            border-width: 0;
            outline: 0;
            color: inherit;
            background-color: transparent;
            vertical-align: top;
            opacity: 1
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 2px 0 1px;
            height: auto;
            width: 100%;
            border-width: 0;
            outline: 0;
            color: inherit;
            background-color: transparent;
            vertical-align: top;
            opacity: 1
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 2px 0 1px;
            height: auto;
            width: 100%;
            border-width: 0;
            outline: 0;
            color: inherit;
            background-color: transparent;
            vertical-align: top;
            opacity: 1
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 2px 0 1px;
            height: auto;
            width: 100%;
            border-width: 0;
            outline: 0;
            color: inherit;
            background-color: transparent;
            vertical-align: top;
            opacity: 1
        }

        .RadComboBox .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px
        }

        .RadComboBox .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px
        }

        .RadComboBox .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px
        }

        .RadComboBox .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px
        }

        .RadComboBox .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px
        }

        .RadComboBox .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px
        }

        .RadComboBox .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px
        }

        .RadComboBox .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbArrowCellRight {
            background-position: -18px -176px
        }

        .RadComboBox .rcbArrowCell {
            width: 18px
        }

        .RadComboBox .rcbArrowCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbArrowCellRight {
            background-position: -18px -176px
        }

        .RadComboBox .rcbArrowCell {
            width: 18px
        }

        .RadComboBox .rcbArrowCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbArrowCellRight {
            background-position: -18px -176px
        }

        .RadComboBox .rcbArrowCell {
            width: 18px
        }

        .RadComboBox .rcbArrowCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbArrowCellRight {
            background-position: -18px -176px
        }

        .RadComboBox .rcbArrowCell {
            width: 18px
        }

        .RadComboBox .rcbArrowCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbArrowCellRight {
            background-position: -18px -176px
        }

        .RadComboBox .rcbArrowCell {
            width: 18px
        }

        .RadComboBox .rcbArrowCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbArrowCellRight {
            background-position: -18px -176px
        }

        .RadComboBox .rcbArrowCell {
            width: 18px
        }

        .RadComboBox .rcbArrowCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbArrowCellRight {
            background-position: -18px -176px
        }

        .RadComboBox .rcbArrowCell {
            width: 18px
        }

        .RadComboBox .rcbArrowCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.914.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')
        }

        .RadComboBox .rcbArrowCellRight {
            background-position: -18px -176px
        }

        .RadComboBox .rcbArrowCell {
            width: 18px
        }

        .RadComboBox .rcbArrowCell {
            padding: 0;
            border-width: 0;
            border-style: solid;
            background-color: transparent;
            background-repeat: no-repeat
        }

        .auto-style2 {
            border-width: 0;
            padding-left: 5px;
            padding-right: 4px;
            padding-top: 0;
            padding-bottom: 0;
        }

        .auto-style3 {
            border-width: 0;
            padding: 0;
        }

        .Btn2 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #ea0000;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #ea0000;
        }

            .Btn2:hover {
                background: white;
                color: #ea0000;
            }

        .Btn3 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #ea0088;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #ea0088;
        }

            .Btn3:hover {
                background: white;
                color: #ea0088;
            }


        .Btn4 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #7900ea;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #7900ea;
        }

            .Btn4:hover {
                background: white;
                color: #7900ea;
            }

        .Btn5 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #00afea;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #00afea;
        }

            .Btn5:hover {
                background: white;
                color: #00afea;
            }

        .Btn6 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #00d407;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #00d407;
        }

            .Btn6:hover {
                background: white;
                color: #00d407;
            }

        .Btn10 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #619fed;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #92baec;
        }

            .Btn10:hover {
                background: #619fed;
                color: white;
                border: solid 2px #2c75d0;
            }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <uc:Menu ID="Menu" runat="server" />
        <telerik:RadTabStrip ID="RT" runat="server" BackColor="#d2eaf6" AutoPostBack="True" SelectedIndex="1">
            <Tabs>
                <telerik:RadTab Text="仕入一覧" Font-Size="12pt" NavigateUrl="ApproptiateList.aspx" Selected="true">
                </telerik:RadTab>
                <telerik:RadTab Text="仕入計上" Font-Size="12pt" NavigateUrl="AppropriateInput.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <br />
        <table style="background-color: #c4ddff;">
            <tr>
                <td class="kMenu" style="background-color: #006bff; color: white;">
                    <p>仕入伝票</p>
                </td>
                <td>
                    <asp:DropDownList ID="DrpFlg" runat="server" Width="120">
                        <asp:ListItem Selected="True" Value="False">発注書未作成</asp:ListItem>
                        <asp:ListItem Value="True">発注書作成済</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="kMenu" runat="server" style="background-color: #006bff; color: white;">
                    <p>カテゴリー</p>
                </td>
                <td>
                    <telerik:RadComboBox ID="RadCate" runat="server" Width="120" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadCate_ItemsRequested"></telerik:RadComboBox>
                </td>
                <td class="kMenu" style="background-color: #006bff; color: white;">
                    <p>仕入先名</p>
                </td>
                <td>
                    <telerik:RadComboBox ID="RadShiire" runat="server" Width="120" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadShiire_ItemsRequested"></telerik:RadComboBox>
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnSerch" Text="検索" OnClick="BtnSerch_Click" CssClass="Btn10" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button runat="server" ID="BtnCSV" Text="CSVダウンロード" CssClass="Btn10" Width="180px" />
        <asp:Button runat="server" ID="BtnDenpyou" Text="仕入伝票" CssClass="Btn10" Width="120px" OnClick="BtnDenpyou_Click" />
        <asp:Button runat="server" ID="BtnEdit" Text="仕入計上" CssClass="Btn10" Width="120px" OnClick="BtnEdit_Click" />
        <br />
        <asp:Label runat="server" ID="err" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <div>
            <telerik:RadGrid ID="RadG" runat="server" CssClass="def" PageSize="1000" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" AutoGenerateColumns="False" OnItemDataBound="RadG_ItemDataBound" OnPageIndexChanged="RadG_PageIndexChanged" OnItemCreated="RadG_ItemCreated">
                <PagerStyle Position="Top" AlwaysVisible="true" BackColor="#dfecfe" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
                <HeaderStyle Font-Size="8" HorizontalAlign="Center" CssClass="hd yt st" />
                <ItemStyle Wrap="true" VerticalAlign="Middle" Font-Size="8" />
                <HeaderContextMenu EnableEmbeddedBaseStylesheet="False" CssClass="GridContextMenu GridContextMenu_Outlook">
                </HeaderContextMenu>
                <AlternatingItemStyle Font-Size="9" CssClass="alterRow" />
                <MasterTableView CellPadding="2" GridLines="Both" BorderWidth="1" BorderColor="#000000" CellSpacing="0" AutoGenerateColumns="False" AllowMultiColumnSorting="false" AllowNaturalSort="false">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ColChk_Row">
                            <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                            <HeaderStyle Width="24" />
                            <ItemTemplate>
                                <input id="ChkRow" runat="server" type="checkbox" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColNo" HeaderText="仕入No">
                            <HeaderStyle Width="50" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColCategory" HeaderText="カテゴリ">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColShiiresakiCode" HeaderText="仕入先コード">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColShiire" HeaderText="仕入先名">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSuryou" HeaderText="数量">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColShiireKingaku" HeaderText="仕入金額">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColCreateDate" HeaderText="仕入日">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>



                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False" EnableEmbeddedBaseStylesheet="False">
                </FilterMenu>
                <ClientSettings>
                    <ClientEvents OnGridCreated="Core.ResizeRadGrid" />
                    <Scrolling AllowScroll="True" FrozenColumnsCount="0" ScrollHeight="600px" EnableColumnClientFreeze="True" UseStaticHeaders="True" />

                </ClientSettings>
            </telerik:RadGrid>
            <input type="hidden" id="count" runat="server" />
            <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
                <%--<ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" /> --%>
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="BtnL">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="TblK" />
                            <telerik:AjaxUpdatedControl ControlID="TblList" LoadingPanelID="LP"
                                UpdatePanelHeight="200px" UpdatePanelRenderMode="Inline" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>

        </div>

    </form>
</body>
</html>
