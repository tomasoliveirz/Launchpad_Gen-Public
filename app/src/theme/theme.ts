import { createSystem, defaultConfig, defineConfig, mergeConfigs } from "@chakra-ui/react"

const theme = defineConfig({
  theme: {
    tokens: {
      colors: {
        bg:{
            primary:{ value: "#353941"},
            secondary:{value:"#26282B"},
            action:{
              1: {value:"#FF8D0F"}
            }
        },
        nav:{
          active:{value: "#ffffff"}
        },
        text:{
          primary:{ value: "#ffffff" }
        },
        detail: { value: "#931621" },
        secondary: { value: "#fff" },
        confirm: { value: "#33673B" },
        error: { value: "#D0273B" },
        warning: { value: "#ECFEAA" },
        info: { value: "#43BCCD" },
        cancel: { value: "#4A4A4A" },
        input: { 
          light: {value: "#00000015"}
        }
      },
    },
    semanticTokens: {
      colors: {
        light: {
          solid: { value: "#00000015" },
          contrast: { value: "#00000015" },
          fg: { value: "#273444" },
          muted: { value: "#00000015" },
          subtle: { value: "#00000015" },
          emphasized: { value: "#00000015" },
          focusRing: { value: "#00000015" },
        }
      }
    }
  },
})

const config = mergeConfigs(defaultConfig, theme);
const system = createSystem(config);

export default system;