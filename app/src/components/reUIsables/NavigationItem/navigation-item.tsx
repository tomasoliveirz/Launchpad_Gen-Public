import { IconType } from "react-icons"

export interface NavigationItem
{
    label:string
    icon?:IconType|string
    url:string
    value?:string
    items?:NavigationItem[]
    element?: React.ComponentType
}