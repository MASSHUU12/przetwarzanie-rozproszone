import {
  useEffect,
  useRef,
  useState,
  type ReactNode,
  type MouseEvent as ReactMouseEvent,
} from "react";
import { Box, styled } from "@mui/material";

interface SplitPaneProps {
  /** "vertical" = top/bottom, "horizontal" = left/right */
  direction?: "vertical" | "horizontal";
  /** initial height of the top pane in pixels (or % string) */
  initialSize: number | string;
  /** minimum height of the top pane (px) */
  minPrimary?: number;
  /** minimum height of the bottom pane (px) */
  minSecondary?: number;
  /** height of the divider (px) */
  dividerSize?: number;
  /** children[0] = top pane, children[1] = bottom pane */
  children: [ReactNode, ReactNode];
}

const Container = styled(Box)`
  display: flex;
  flex-direction: ${(props: { direction: string }) =>
    props.direction === "vertical" ? "column" : "row"};
  height: 100%;
  overflow: hidden;
`;

const Pane = styled(Box)`
  flex: none;
  width: ${(props: { direction: string; size: number | string }) =>
    props.direction === "vertical"
      ? "100%"
      : typeof props.size === "number"
        ? `${props.size}px`
        : props.size};
  height: ${(props: { direction: string; size: number | string }) =>
    props.direction === "vertical"
      ? typeof props.size === "number"
        ? `${props.size}px`
        : props.size
      : "100%"};
  overflow: hidden;
`;

const Divider = styled(Box, {
  shouldForwardProp: (prop) => prop !== "dividerSize",
})<{
  direction: string;
  dividerSize: number;
}>`
  flex: none;
  cursor: ${(props: { direction: string }) =>
    props.direction === "vertical" ? "row-resize" : "col-resize"};
  background-color: ${(props) => props.theme.palette.divider};
  width: ${(props: { direction: string; dividerSize: number }) =>
    props.direction === "vertical" ? "100%" : `${props.dividerSize}px`};
  height: ${(props: { direction: string; dividerSize: number }) =>
    props.direction === "vertical" ? `${props.dividerSize}px` : "100%"};
`;

const SecondaryPane = styled(Box)`
  flex: 1;
  overflow: auto;
`;

export default function SplitPane({
  direction = "vertical",
  initialSize,
  minPrimary = 100,
  minSecondary = 100,
  dividerSize = 4,
  children: [First, Second],
  ...rest
}: SplitPaneProps) {
  const containerRef = useRef<HTMLDivElement>(null);
  const [primarySize, setPrimarySize] = useState<number | string>(initialSize);
  const [dragging, setDragging] = useState(false);

  useEffect(() => {
    function onMouseMove(e: MouseEvent) {
      if (!dragging || !containerRef.current) return;
      const rect = containerRef.current.getBoundingClientRect();

      let newSize: number;
      if (direction === "vertical") {
        newSize = e.clientY - rect.top;
        newSize = Math.max(
          minPrimary,
          Math.min(newSize, rect.height - minSecondary),
        );
      } else {
        newSize = e.clientX - rect.left;
        newSize = Math.max(
          minPrimary,
          Math.min(newSize, rect.width - minSecondary),
        );
      }

      setPrimarySize(newSize);
    }

    function onMouseUp() {
      setDragging(false);
    }

    window.addEventListener("mousemove", onMouseMove);
    window.addEventListener("mouseup", onMouseUp);
    return () => {
      window.removeEventListener("mousemove", onMouseMove);
      window.removeEventListener("mouseup", onMouseUp);
    };
  }, [dragging, direction, minPrimary, minSecondary]);

  const handleMouseDown = (e: ReactMouseEvent) => {
    e.preventDefault();
    setDragging(true);
  };

  return (
    <Container ref={containerRef} direction={direction} {...rest}>
      <Pane direction={direction} size={primarySize}>
        {First}
      </Pane>
      <Divider
        direction={direction}
        dividerSize={dividerSize}
        onMouseDown={handleMouseDown}
      />
      <SecondaryPane>{Second}</SecondaryPane>
    </Container>
  );
}
