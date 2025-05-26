import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import LockIcon from "@mui/icons-material/Lock";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Stack,
  Typography,
} from "@mui/material";
import React, { useState } from "react";
import { endpoints, type Endpoint } from "../api/endpoints";

const groupByCategory = (data: Endpoint[]) => {
  return data.reduce(
    (acc, endpoint) => {
      acc[endpoint.category] = acc[endpoint.category] || [];
      acc[endpoint.category].push(endpoint);
      return acc;
    },
    {} as Record<string, Endpoint[]>,
  );
};

interface EndpointsByCategoryProps {
  onEndpointClick?: (endpoint: Endpoint) => void;
}

export default function EndpointsByCategory({
  onEndpointClick,
}: EndpointsByCategoryProps) {
  const grouped = groupByCategory(endpoints);
  const categoryList = Object.keys(grouped);

  const [expanded, setExpanded] = useState<string | false>(false);

  const handleChange =
    (panel: string) => (_event: React.SyntheticEvent, isExpanded: boolean) => {
      setExpanded(isExpanded ? panel : false);
    };

  return (
    <Box>
      {categoryList.map((cat) => (
        <Accordion
          key={cat}
          expanded={expanded === cat}
          onChange={handleChange(cat)}
          sx={{ mb: 2 }}
        >
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <Typography variant="h6" fontWeight="bold">
              {cat}
            </Typography>
          </AccordionSummary>
          <AccordionDetails>
            <List dense>
              {grouped[cat].map((ep) => (
                <ListItem disablePadding key={ep.value + ep.method}>
                  <ListItemButton
                    onClick={() => onEndpointClick?.(ep)}
                    sx={{ borderRadius: 1 }}
                  >
                    <ListItemText
                      primary={
                        <Stack direction="row" spacing={1}>
                          <strong>{ep.name}</strong>{" "}
                          <Typography
                            component="span"
                            variant="body2"
                            color="text.secondary"
                          >
                            ({ep.method})
                          </Typography>
                          <LockIcon />
                        </Stack>
                      }
                    />
                  </ListItemButton>
                </ListItem>
              ))}
            </List>
          </AccordionDetails>
        </Accordion>
      ))}
    </Box>
  );
}
