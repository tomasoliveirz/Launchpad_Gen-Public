import GaugeComponent from "react-gauge-component";

export interface GaugeChartProps {
    value: number
    arrowType?: "arrow" | "needle" | "blob"
    width?: string
}
export function LaunchpadGaugeComponent({ value, width, arrowType = "arrow" }: GaugeChartProps) {
    return <GaugeComponent
        style={{ width: width }}
        type="semicircle"
        arc={{
            colorArray: ['#00FF15', '#FF2121'],
            padding: 0.05,
            nbSubArcs: 20
        }}
        pointer={{ type: arrowType, animationDelay: 0 }}
        value={value}
    />
}
