import { NavigationItem } from "../NavigationItem/navigation-item";
import {Breadcrumb, Text } from "@chakra-ui/react";
import React from "react";
import { ImageOrIcon } from "../ImageOrIcon/image-or-icon";

export interface BreadcrumbsProps
{
    items?:NavigationItem[]
}

function BreadcrumbNavigationItem(item:NavigationItem)
{
  return <>
          <ImageOrIcon mr={item.label ? "5px":"0px"} w="1rem" value={item.icon}/>
          <Text>{item.label}</Text>
        </>
}

export function Breadcrumbs({items, ...props}:BreadcrumbsProps){
    return items ? <Breadcrumb.Root {...props}>
                <Breadcrumb.List>
                    {items.map((x:NavigationItem, idx:number)=>{
                        return <React.Fragment key={"breadcrumb"+idx}>
                            <Breadcrumb.Item>
                                {(idx < items.length-1) ? <Breadcrumb.Link href={x.url} fontWeight="bold" color="text.primary">
                                <BreadcrumbNavigationItem {...x}/>
                                </Breadcrumb.Link>:<BreadcrumbNavigationItem {...x}/>}
                                
                            </Breadcrumb.Item>
                            {(idx < items.length-1) && <Breadcrumb.Separator/>}
                        </React.Fragment>
                    })}
                </Breadcrumb.List>
              </Breadcrumb.Root>:<></>
}


export function getBreadcrumbs(pages:NavigationItem[], path:string, idSubstitutes?:NavigationItem[]):NavigationItem[]
{
  let crumbPath: NavigationItem[] = [];
  let subCounter = 0;
  let basePath = "";
  const pathTokens = path.split("/");
  for(let i = 0; i<pathTokens.length; i++)
  {
    basePath+=(i===0 ? "":"/");
    const isGuid = isGUID(pathTokens[i]);
    if(isGuid && idSubstitutes && idSubstitutes.length > subCounter)
    {
      let page = idSubstitutes[subCounter];
      
      basePath+="uuid";
      if(page)
      {
        crumbPath.push({...page, url:page.url.replace("uuid", pathTokens[i])});
      }
      subCounter++;
    }
    else
    {

      basePath+=pathTokens[i];
      console.log(basePath);
      const page = pages.find(x => x.url === basePath);
      if(page) crumbPath.push(page);
    }
  }

  return crumbPath;
}

export function isGUID(value:string)
 {
    const guidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
    return guidRegex.test(value);
}