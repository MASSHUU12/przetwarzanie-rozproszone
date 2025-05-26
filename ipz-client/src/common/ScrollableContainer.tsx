import type { ReactNode, CSSProperties } from "react";

interface Props {
  children: ReactNode;
  maxHeight?: string;
  style?: CSSProperties;
  className?: string;
}

function ScrollableContainer({
  children,
  maxHeight = "300px",
  style,
  className,
}: Props) {
  const containerStyle: CSSProperties = {
    overflowY: "auto",
    maxHeight,
    ...style,
  };

  return (
    <div style={containerStyle} className={className}>
      {children}
    </div>
  );
}

export default ScrollableContainer;
